import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import CreateVirtualMachineRequest from "../requests/CreateVirtualMachineRequest.ts";
import VirtualMachinesEntitiesResponse from "../responses/VirtualMachinesEntitiesResponse.ts";
import CreateVirtualNetworkNodeRequest from "../requests/CreateVirtualNetworkNodeRequest.ts";
import VirtualNetworkNodesResponse from "../responses/VirtualNetworkNodesResponse.ts";
import StringResponse from "../responses/StringResponse.ts";
import UpdateEntityPositionRequest from "../requests/UpdateEntityPositionRequest.ts";
import InternetEntitiesResponse from "../responses/InternetEntitiesResponse.ts";
import VirtualMachineEntityResponse from "../responses/VirtualMachineEntityResponse.ts";
import DeleteEntityRequest from "../requests/DeleteEntityRequest.ts";

/**
 * Resource client for managing entities (virtual machines, networks, Internet) in the simulation.
 */
class EntityResourceClient extends BaseBackendResourceClient {
    /**
     * Creates a new virtual machine entity.
     * @param {CreateVirtualMachineRequest} req The virtual machine creation request
     * @returns {Promise<VirtualMachineEntityResponse>} The created virtual machine entity response
     */
    public createVirtualMachineEntity(
        req: CreateVirtualMachineRequest,
    ): Promise<VirtualMachineEntityResponse> {
        return this.sendBackendRequest(
            "CreateVirtualMachineEntity",
            "POST",
            req,
        );
    }

    /**
     * Creates a new virtual network node entity.
     * @param {CreateVirtualNetworkNodeRequest} req The network node creation request
     * @returns {Promise<VirtualNetworkNodesResponse>} The created network node entity response
     */
    public createVirtualNetworkNodeEntity(
        req: CreateVirtualNetworkNodeRequest,
    ): Promise<VirtualNetworkNodesResponse> {
        return this.sendBackendRequest(
            "CreateVirtualNetworkNodeEntity",
            "POST",
            req,
        );
    }

    /**
     * Creates a new Internet entity.
     * @returns {Promise<VirtualNetworkNodesResponse>} The created Internet entity response
     */
    public createInternetEntity(): Promise<VirtualNetworkNodesResponse> {
        return this.sendBackendRequest("CreateInternetEntity", "POST", {});
    }

    /**
     * Updates the position of an entity in the simulation stage.
     * @param {UpdateEntityPositionRequest} req The position update request
     * @returns {Promise<StringResponse>} Success message response
     */
    public updateEntityPosition(
        req: UpdateEntityPositionRequest,
    ): Promise<StringResponse> {
        return this.sendBackendRequest("UpdateEntityPosition", "PUT", req);
    }

    /**
     * Retrieves all virtual machine entities.
     * @returns {Promise<VirtualMachinesEntitiesResponse>} All virtual machine entities with MAC addresses
     */
    public getAllVirtualMachineEntities(): Promise<VirtualMachinesEntitiesResponse> {
        return this.sendBackendRequest(
            "GetAllVirtualMachineEntities",
            "GET",
            {},
        );
    }

    /**
     * Retrieves all virtual network node entities.
     * @returns {Promise<VirtualNetworkNodesResponse>} All network node entities
     */
    public getAllVirtualNetworkNodeEntities(): Promise<VirtualNetworkNodesResponse> {
        return this.sendBackendRequest(
            "GetAllVirtualNetworkNodeEntities",
            "GET",
            {},
        );
    }

    /**
     * Retrieves all Internet entities.
     * @returns {Promise<InternetEntitiesResponse>} All Internet entities
     */
    public getAllInternetEntities(): Promise<InternetEntitiesResponse> {
        return this.sendBackendRequest("GetAllInternetEntities", "GET", {});
    }

    public deleteEntity(request: DeleteEntityRequest): Promise<StringResponse> {
        return this.sendBackendRequest("DeleteEntity", "DELETE", request);
    }

    protected getResourceName(): string {
        return "Entity";
    }
}

const instance = new EntityResourceClient();

export default instance;
