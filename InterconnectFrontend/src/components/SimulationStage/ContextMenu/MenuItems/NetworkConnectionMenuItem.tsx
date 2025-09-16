import { Menu } from "@mantine/core";
import { MdOutlineCable } from "react-icons/md";
import { useTranslation } from "react-i18next";

interface NetworkConnectionMenuItemProps {
    entityName: string;
    onClick: () => void;
}

const NetworkConnectionMenuItem = ({
    entityName,
    onClick,
}: NetworkConnectionMenuItemProps) => {
    const { t } = useTranslation();

    return (
        <Menu.Item
            onClick={onClick}
            key={entityName}
            leftSection={<MdOutlineCable size={14} color="red" />}
        >
            {t("disconnectWith", { entityName })}
        </Menu.Item>
    );
};

export default NetworkConnectionMenuItem;
