/**
 * Represents terminal output data from a virtual machine console.
 */
interface TerminalDataModel {
    /** UUID of the virtual machine that produced this terminal data */
    uuid: string;
    /** The terminal data content (command output, console text, etc.) */
    data: string;
}

export type { TerminalDataModel };
