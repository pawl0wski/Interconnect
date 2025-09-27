using Mappers;
using Models.Database;
using Models.DTO;
using Models.Enums;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualSwitchConnector : IVirtualSwitchConnector
    {
        private record EntityTypeWithId
        {
            public required int Id;
            public required EntityType Type;

            public bool EqualsTo(int id, EntityType type)
                => Id == id && Type == type;
        };

        private readonly IVirtualSwitchEntityRepository _switchRepository;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IInternetEntityRepository _internetEntityRepository;
        private readonly IVirtualNetworkService _virtualNetworkService;

        public VirtualSwitchConnector(
            IVirtualSwitchEntityRepository switchRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualNetworkRepository networkRepository,
            IInternetEntityRepository internetEntityRepository,
            IVirtualNetworkService virtualNetworkService)
        {
            _switchRepository = switchRepository;
            _connectionRepository = connectionRepository;
            _networkRepository = networkRepository;
            _internetEntityRepository = internetEntityRepository;
            _virtualNetworkService = virtualNetworkService;
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualSwitches(int sourceVirtualSwitch, int destinationVirtualSwitch)
        {
            var sourceVirtualSwitchEntities = await GetAllConnectedEntitiesToSwitch(sourceVirtualSwitch, null);
            var destinationVirtualSwitchEntities = await GetAllConnectedEntitiesToSwitch(destinationVirtualSwitch, null);

            var popularNetwork = await GetPopularNetwork([.. sourceVirtualSwitchEntities, .. destinationVirtualSwitchEntities]);
            await ConnectEntitiesToNetwork(sourceVirtualSwitchEntities, popularNetwork);
            await ConnectEntitiesToNetwork(destinationVirtualSwitchEntities, popularNetwork);

            var connectionModel = await _connectionRepository.Create(sourceVirtualSwitch, EntityType.VirtualSwitch, destinationVirtualSwitch, EntityType.VirtualSwitch);
            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task DisconnectTwoVirtualSwitches(int connectionId, int firstVirtualSwitchId, int secondVirtualSwitch)
        {
            var firstVirtualSwitchEntities = await GetAllConnectedEntitiesToSwitch(firstVirtualSwitchId, connectionId);
            var secondVirtualSwitchEntities = await GetAllConnectedEntitiesToSwitch(secondVirtualSwitch, connectionId);

            await ConnectDisconnectedEntities(firstVirtualSwitchEntities);
            await ConnectDisconnectedEntities(secondVirtualSwitchEntities);

            await _connectionRepository.Remove(connectionId);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualSwitchToInternet(int virtualSwitchId, int internetId)
        {
            var virtualSwitchConnectedEntities = await GetAllConnectedEntitiesToSwitch(virtualSwitchId, null);

            var internetNetwork = (await _internetEntityRepository.GetById(internetId)).VirtualNetwork;
            await ConnectEntitiesToNetwork(virtualSwitchConnectedEntities, internetNetwork);

            var connectionModel = await _connectionRepository.Create(virtualSwitchId, EntityType.VirtualSwitch, internetId, EntityType.Internet);
            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task DisconnectVirtualSwitchFromInternet(int connectionId, int virtualSwitchId)
        {
            var virtualSwitchConnectedEntities = await GetAllConnectedEntitiesToSwitch(virtualSwitchId, connectionId);

            await ConnectDisconnectedEntities(virtualSwitchConnectedEntities);

            await _connectionRepository.Remove(connectionId);
        }

        private async Task<List<EntityTypeWithId>> GetAllConnectedEntitiesToSwitch(int virtualSwitchId, int? connectionIdToSkip)
        {
            var virtualSwitch = VirtualSwitchEntityMapper.MapToDTO(await _switchRepository.GetById(virtualSwitchId));
            Queue<VirtualNetworkEntityConnectionModel> connetionsToVisit = new();
            HashSet<VirtualNetworkEntityConnectionModel> visitedConnections = [];
            HashSet<EntityTypeWithId> visitiedEntities = [new EntityTypeWithId { Id = virtualSwitch.Id, Type = EntityType.VirtualSwitch }];

            (await _connectionRepository.GetUsingEntityId(virtualSwitchId, EntityType.VirtualSwitch)).ForEach(connetionsToVisit.Enqueue);

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
                virtualNetwork = await _virtualNetworkService.CreateSwitchVirtualNetwork();
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
                        await _virtualNetworkService.UpdateNetworkForVirtualMachineNetworkInterface(entity.Id, VirtualNetworkUtils.GetNetworkNameFromUuid(virtualNetwork.Uuid));
                        break;
                    case EntityType.VirtualSwitch:
                        await _switchRepository.UpdateNetwork(entity.Id, virtualNetwork);
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
                    case EntityType.VirtualSwitch:
                        virtualNetwork = (await _switchRepository.GetById(entity.Id)).VirtualNetwork;
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
