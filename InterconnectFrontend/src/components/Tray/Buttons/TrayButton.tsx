import { ReactNode } from "react";
import { Button, Flex, Text } from "@mantine/core";

/**
 * Props for the `TrayButton` component.
 */
interface TrayButtonProps {
    active: boolean;
    icon: ReactNode;
    text: string;
    onClick: () => void;
}

/**
 * Generic tray button used for entity creation actions.
 * Switches between filled and outline variants based on `active`.
 * @param props Component props
 * @param props.active Whether the button is active/selected
 * @param props.icon Icon node to render above the label
 * @param props.text Button label text
 * @param props.onClick Click handler
 * @returns A styled Mantine button with vertical icon+label
 */
const TrayButton = ({ active, icon, text, onClick }: TrayButtonProps) => {
    return (
        <Button
            onClick={onClick}
            variant={active ? "filled" : "outline"}
            w={170}
            h="100%"
        >
            <Flex direction="column" align="center" justify="space-between">
                {icon}
                <Text size="sm">{text}</Text>
            </Flex>
        </Button>
    );
};

export default TrayButton;
