import TrayButton from "./TrayButton.tsx";
import { MdLanguage } from "react-icons/md";
import { useTranslation } from "react-i18next";

interface InternetEntityTrayButtonProps {
    active: boolean;
    onClick: () => void;
}

const InternetEntityTrayButton = ({
    active,
    onClick,
}: InternetEntityTrayButtonProps) => {
    const { t } = useTranslation();

    return (
        <TrayButton
            active={active}
            icon={<MdLanguage size={30} />}
            text={t("internet.internet")}
            onClick={onClick}
        />
    );
};

export default InternetEntityTrayButton;
