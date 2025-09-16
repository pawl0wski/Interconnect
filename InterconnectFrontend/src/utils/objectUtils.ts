const ObjectUtils = {
    getValueOrNull<T>(obj: Record<string, unknown>, key: string): T | null {
        if (!Object.prototype.hasOwnProperty.call(obj, key)) {
            return null;
        }
        const value = obj[key];
        return value === undefined ? null : (value as T);
    },
};

export default ObjectUtils;
