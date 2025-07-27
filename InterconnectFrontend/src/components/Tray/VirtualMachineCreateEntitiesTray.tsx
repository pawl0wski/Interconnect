import { Button } from "@mantine/core";

interface VirtualMachineCreateEntitiesTrayProps {
    onClick: () => void;
}

const VirtualMachineCreateEntitiesTray = ({ onClick }: VirtualMachineCreateEntitiesTrayProps) => {
    return <Button onClick={onClick}>Dodaj maszyne wirtualną</Button>;
};

export default VirtualMachineCreateEntitiesTray;