import bytes from "bytes";

/**
 * Utility class for parsing and converting memory values between human-readable and byte formats.
 */
const MemoryValueParser = {
    /**
     * Converts a memory value in MB to a human-readable string format (e.g., "1GB", "512MB").
     * @param {number} memory The memory value in MB
     * @returns {string | null} The human-readable memory string, or null if conversion fails
     */
    valueMemoryToHumanReadable(memory: number): string | null {
        return bytes(memory * 1024);
    },
    /**
     * Converts a human-readable memory string to its value in MB.
     * @param {string} memory The human-readable memory string (e.g., "1GB", "512MB")
     * @returns {number | null} The memory value in MB, or null if parsing fails
     */
    humanReadableMemoryToValue(memory: string): number | null {
        const parsedValue = bytes.parse(memory);
        if (parsedValue === null) {
            return null;
        }
        return parsedValue / 1024;
    },
};

export default MemoryValueParser;
