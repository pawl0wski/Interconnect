/**
 * Utility class for object manipulation operations.
 */
const ObjectUtils = {
    /**
     * Safely retrieves a value from an object with type safety.
     * Returns null if the key doesn't exist or the value is undefined.
     * @template T The expected type of the value
     * @param {Record<string, unknown>} obj The object to retrieve from
     * @param {string} key The property key
     * @returns {T | null} The value cast to type T, or null
     */
    getValueOrNull<T>(obj: Record<string, unknown>, key: string): T | null {
        if (!Object.prototype.hasOwnProperty.call(obj, key)) {
            return null;
        }
        const value = obj[key];
        return value === undefined ? null : (value as T);
    },
};

export default ObjectUtils;
