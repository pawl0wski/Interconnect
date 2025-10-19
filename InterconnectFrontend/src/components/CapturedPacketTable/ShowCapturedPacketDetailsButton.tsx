import { MdOpenInNew } from "react-icons/md";
import { Button } from "@mantine/core";

interface ShowCapturedPacketDetailsButtonProps {
    onClick: () => void;
}

const ShowCapturedPacketDetailsButton = ({
    onClick,
}: ShowCapturedPacketDetailsButtonProps) => (
    <Button variant="transparent" size="md" onClick={onClick}>
        <MdOpenInNew />
    </Button>
);

export default ShowCapturedPacketDetailsButton;
