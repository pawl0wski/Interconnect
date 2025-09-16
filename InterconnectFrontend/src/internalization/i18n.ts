import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import pl from "./langs/pl";

i18n.use(initReactI18next).init({
    resources: {
        pl: {
            translation: pl,
        },
    },
    lng: "pl",
});
