import { Text } from "react-konva";
import CommunicationRole from "../../models/enums/CommunicationRole.ts";

interface SenderRecipientEntityDescriptionProps {
    y: number;
    role?: CommunicationRole;
}

const CommunicationRoleEntityDescription = ({
    y,
    role,
}: SenderRecipientEntityDescriptionProps) => {
    return (
        role !== undefined && (
            <Text
                y={y}
                fontStyle="normal"
                align="center"
                width={55}
                fill={role === CommunicationRole.Sender ? "blue" : "#d55429"}
                text={
                    role === CommunicationRole.Sender ? "Nadawca" : "Odbiorca"
                }
            />
        )
    );
};

export default CommunicationRoleEntityDescription;
