import bytes from "bytes";

class MemoryValueParser {
    public static valueMemoryToHumanReadable(memory: number): string | null {
        return bytes(memory * 1024);
    }

    public static humanReadableMemoryToValue(memory: string): number | null {
        const parsedValue = bytes.parse(memory);
        if (parsedValue === null) {
            return null;
        }
        return parsedValue / 1024;
    }
}

export default MemoryValueParser;