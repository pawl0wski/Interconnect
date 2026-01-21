/**
 * Enumeration of virtual machine entity types/roles in the simulation.
 */
enum VirtualMachineEntityType {
    /** Virtual machine acting as a host */
    Host = 1,
    /** Virtual machine acting as a router */
    Router = 2,
    /** Virtual machine acting as a server */
    Server = 3,
}

export default VirtualMachineEntityType;
