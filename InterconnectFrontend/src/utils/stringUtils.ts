/**
 * Utility class for string manipulation operations.
 */
const StringUtils = {
    /**
     * Capitalizes the first character of a string.
     * @param {string} str The input string
     * @returns {string} The string with the first character capitalized
     */
    capitalizeString(str: string): string {
        return str.charAt(0).toUpperCase() + str.slice(1);
    },
};

export default StringUtils;
