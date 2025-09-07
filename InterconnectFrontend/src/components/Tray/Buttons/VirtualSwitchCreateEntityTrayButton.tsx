import { useTranslation } from "react-i18next";
import TrayButton from "./TrayButton.tsx";
import switchButton from "../../../static/switchButton.svg";
import switchButtonActive from "../../../static/switchButtonActive.svg";

interface VirtualSwitchCreateEntityTrayButtonProps {
    active: boolean;
    onClick: () => void;
}

const VirtualSwitchCreateEntityTrayButton = ({ active, onClick }: VirtualSwitchCreateEntityTrayButtonProps) => {
    const { t } = useTranslation();

    return <TrayButton
        active={active}
        icon={<img width={30} height={30} src={active ? switchButtonActive : switchButton}
                   alt={t("virtualSwitch.virtualSwitch")} />}
        text={t("virtualSwitch.virtualSwitch")}
        onClick={onClick} />;
};

export default VirtualSwitchCreateEntityTrayButton;