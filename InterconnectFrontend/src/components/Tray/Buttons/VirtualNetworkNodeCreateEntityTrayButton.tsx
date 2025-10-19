import { useTranslation } from "react-i18next";
import TrayButton from "./TrayButton.tsx";
import nodeButton from "../../../static/nodeButton.svg";
import nodeButtonActive from "../../../static/nodeButtonActive.svg";

interface VirtualNetworkNodeCreateEntityTrayButtonProps {
    active: boolean;
    onClick: () => void;
}

const VirtualNetworkNodeCreateEntityTrayButton = ({
    active,
    onClick,
}: VirtualNetworkNodeCreateEntityTrayButtonProps) => {
    const { t } = useTranslation();

    return (
        <TrayButton
            active={active}
            icon={
                <img
                    width={30}
                    height={30}
                    src={active ? nodeButtonActive : nodeButton}
                    alt={t("virtualNetworkNode.virtualNetworkNode")}
                />
            }
            text={t("virtualNetworkNode.virtualNetworkNode")}
            onClick={onClick}
        />
    );
};

export default VirtualNetworkNodeCreateEntityTrayButton;
