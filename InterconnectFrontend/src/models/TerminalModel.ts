/**
 * Represents terminal data associated with a virtual machine.
 */
interface TerminalModel {
    /** UUID of the virtual machine associated with the terminal */
    uuid: string;
    /** Terminal output or command data */
    data: string;
}

export type { TerminalModel };
