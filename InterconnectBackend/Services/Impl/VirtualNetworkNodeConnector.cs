using Mappers;
using Models.Database;
using Models.DTO;
using Models.Enums;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualNetworkNodeConnector : IVirtualNetworkNodeConnector
    {
        private record EntityTypeWithId
        {
            public required int Id;
            public required EntityType Type;

            public bool EqualsTo(int id, EntityType type)
                => Id == id && Type == type;
        };

        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeRepository;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IInternetEntityRepository _internetEntityRepository;
        private readonly IVirtualNetworkService _virtualNetworkService;
        private readonly IVirtualMachineEntityNetworkInterfaceRepository _networkInterfaceRepository;

        public VirtualNetworkNodeConnector(
            IVirtualNetworkNodeEntityRepository virtualNetworkNodeRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualNetworkRepository networkRepository,
            IInternetEntityRepository internetEntityRepository,
            IVirtualNetworkService virtualNetworkService,
            IVirtualMachineEntityNetworkInterfaceRepository networkInterfaceRepository)
        {
            _virtualNetworkNodeRepository = virtualNetworkNodeRepository;
            _connectionRepository = connectionRepository;
            _networkRepository = networkRepository;
            _internetEntityRepository = internetEntityRepository;
            _virtualNetworkService = virtualNetworkService;
            _networkInterfaceRepository = networkInterfaceRepository;
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualNetworkNodes(int sourceVirtualNetworkNode, int destinationVirtualNetworkNode)
        {
            var sourceVirtualNetworkNodeEntities = await GetAllConnectedEntitiesToVirtualNetworkNode(sourceVirtualNetworkNode, null);
            var destinationVirtualNetworkNodeEntities = await GetAllConnectedEntitiesToVirtualNetworkNode(destinationVirtualNetworkNode, null);

            var popularNetwork = await GetPopularNetwork([.. sourceVirtualNetworkNodeEntities, .. destinationVirtualNetworkNodeEntities]);
            await ConnectEntitiesToNetwork(sourceVirtualNetworkNodeEntities, popularNetwork);
            await ConnectEntitiesToNetwork(destinationVirtualNetworkNodeEntities, popularNetwork);

            var connectionModel = await _connectionRepository.Create(sourceVirtualNetworkNode, EntityType.VirtualNetworkNode, destinationVirtualNetworkNode, EntityType.VirtualNetworkNode);
            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task DisconnectTwoVirtualNetworkNodes(int connectionId, int firstVirtualNetworkNodeId, int secondVirtualNetworkNode)
        {
            var firstVirtualNetworkNodeEntities = await GetAllConnectedEntitiesToVirtualNetworkNode(firstVirtualNetworkNodeId, connectionId);
            var secondVirtualNetworkNodeEntities = await GetAllConnectedEntitiesToVirtualNetworkNode(secondVirtualNetworkNode, connectionId);

            await ConnectDisconnectedEntities(firstVirtualNetworkNodeEntities);
            await ConnectDisconnectedEntities(secondVirtualNetworkNodeEntities);

            await _connectionRepository.Remove(connectionId);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualNetworkNodeToInternet(int virtualNetworkNodeId, int internetId)
        {
            var virtualNetworkNodeConnectedEntities = await GetAllConnectedEntitiesToVirtualNetworkNode(virtualNetworkNodeId, null);

            var internetNetwork = (await _internetEntityRepository.GetById(internetId)).VirtualNetwork;
            await ConnectEntitiesToNetwork(virtualNetworkNodeConnectedEntities, internetNetwork);

            var connectionModel = await _connectionRepository.Create(virtualNetworkNodeId, EntityType.VirtualNetworkNode, internetId, EntityType.Internet);
            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task DisconnectVirtualNetworkNodeFromInternet(int connectionId, int virtualNetworkNodeId)
        {
            var virtualNetworkNodeConnectedEntities = await GetAllConnectedEntitiesToVirtualNetworkNode(virtualNetworkNodeId, connectionId);

            await ConnectDisconnectedEntities(virtualNetworkNodeConnectedEntities);

            await _connectionRepository.Remove(connectionId);
        }

        private async Task<List<EntityTypeWithId>> GetAllConnectedEntitiesToVirtualNetworkNode(int virtualNetworkNodeId, int? connectionIdToSkip)
        {
            var virtualNetworkNode = VirtualNetworkNodeEntityMapper.MapToDTO(await _virtualNetworkNodeRepository.GetById(virtualNetworkNodeId));
            Queue<VirtualNetworkEntityConnectionModel> connetionsToVisit = new();
            HashSet<VirtualNetworkEntityConnectionModel> visitedConnections = [];
            HashSet<EntityTypeWithId> visitiedEntities = [new EntityTypeWithId { Id = virtualNetworkNode.Id, Type = EntityType.VirtualNetworkNode }];

            (await _connectionRepository.GetUsingEntityId(virtualNetworkNodeId, EntityType.VirtualNetworkNode)).ForEach(connetionsToVisit.Enqueue);

            while (connetionsToVisit.Count > 0)
            {
                var connection = connetionsToVisit.Dequeue();

                if (connectionIdToSkip is not null && connection.Id == connectionIdToSkip)
                {
                    continue;
                }

                EntityTypeWithId target = visitiedEntities.Any(x => x.EqualsTo(connection.SourceEntityId, connection.SourceEntityType))
                    ? new EntityTypeWithId { Id = connection.DestinationEntityId, Type = connection.DestinationEntityType }
                    : new EntityTypeWithId { Id = connection.SourceEntityId, Type = connection.SourceEntityType };

                if (visitiedEntities.Any(x => x.Equals(target)))
                {
                    continue;
                }

                visitiedEntities.Add(target);

                var connections = await _connectionRepository.GetUsingEntityId(target.Id, target.Type);
                connections.Where(c => !visitedConnections.Contains(c)).ToList().ForEach(connetionsToVisit.Enqueue);
            }

            return [.. visitiedEntities];
        }

        private async Task ConnectDisconnectedEntities(List<EntityTypeWithId> entities)
        {
            EntityTypeWithId? internetEntityWithId = entities.FirstOrDefault(entity => entity.Type == EntityType.Internet);

            VirtualNetworkModel? virtualNetwork = null;

            if (internetEntityWithId is not null)
            {
                var internetEntity = await _internetEntityRepository.GetById(internetEntityWithId.Id);
                virtualNetwork = internetEntity.VirtualNetwork;
            }

            if (virtualNetwork is null)
            {
                virtualNetwork = await _virtualNetworkService.CreateNodeVirtualNetwork();
            }

            await ConnectEntitiesToNetwork(entities, virtualNetwork);
        }

        private async Task ConnectEntitiesToNetwork(List<EntityTypeWithId> entities, VirtualNetworkModel virtualNetwork)
        {
            foreach (var entity in entities)
            {
                switch (entity.Type)
                {
                    case EntityType.VirtualMachine:
                        var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(virtualNetwork.Uuid);
                        var networkInterfaces = await _networkInterfaceRepository.GetByNetworkName(entity.Id, networkName);
                        foreach (var networkInterface in networkInterfaces)
                        {
                            await _virtualNetworkService
                                .UpdateNetworkForVirtualMachineNetworkInterface(entity.Id, networkInterface.VirtualNetworkEntityConnectionId, networkName);
                        }
                        break;
                    case EntityType.VirtualNetworkNode:
                        await _virtualNetworkNodeRepository.UpdateNetwork(entity.Id, virtualNetwork);
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task<VirtualNetworkModel> GetPopularNetwork(List<EntityTypeWithId> entities)
        {
            HashSet<EntityTypeWithId> uniqueEntities = [.. entities];
            Dictionary<int, int> networks = new();
            VirtualNetworkModel? virtualNetwork = null;

            foreach (var entity in uniqueEntities)
            {
                switch (entity.Type)
                {
                    case EntityType.Internet:
                        virtualNetwork = (await _internetEntityRepository.GetById(entity.Id)).VirtualNetwork;
                        networks[virtualNetwork.Id] = int.MaxValue;
                        break;
                    case EntityType.VirtualNetworkNode:
                        virtualNetwork = (await _virtualNetworkNodeRepository.GetById(entity.Id)).VirtualNetwork;
                        networks[virtualNetwork.Id] = networks.GetValueOrDefault(virtualNetwork.Id) + 1;
                        break;
                }
            }

            var popularNetworkId = networks
                .OrderByDescending(n => n.Value)
                .First()
                .Key;
            return await _networkRepository.GetById(popularNetworkId);
        }
    }
}
