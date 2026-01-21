import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import pl from "./langs/pl";

/**
 * Initializes i18next internationalization system for the application.
 * Configures React integration and sets up Polish language as default.
 */
i18n.use(initReactI18next).init({
    resources: {
        pl: {
            translation: pl,
        },
    },
    lng: "pl",
});
