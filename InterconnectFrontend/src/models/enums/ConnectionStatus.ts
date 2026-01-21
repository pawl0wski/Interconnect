/**
 * Enumeration of possible connection statuses to the hypervisor.
 */
enum ConnectionStatus {
    /** Connection is active and alive */
    Alive = 1,
    /** Connection is dead or closed */
    Dead = 0,
    /** Connection status is unknown */
    Unknown = -1,
}

export { ConnectionStatus };
