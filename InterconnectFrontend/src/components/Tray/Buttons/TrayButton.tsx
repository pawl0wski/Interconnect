import { ReactNode } from "react";
import { Button, Flex, Text } from "@mantine/core";

interface TrayButtonProps {
    icon: ReactNode;
    text: string;
    onClick: () => void;
}

const TrayButton = ({ icon, text, onClick }: TrayButtonProps) => {
    return <Button onClick={onClick} variant="outline" w={170} h="100%">
        <Flex direction="column" align="center" justify="space-between">
            {icon}
            <Text size="sm">{text}</Text>
        </Flex>
    </Button>;
};

export default TrayButton;