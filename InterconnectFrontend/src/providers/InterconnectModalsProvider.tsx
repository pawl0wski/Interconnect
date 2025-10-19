import { ReactNode } from "react";
import VirtualMachineCreateModalContainer from "../components/Modals/VirtualMachineCreateModalContainer.tsx";
import VirtualSwitchCreateModalContainer from "../components/Modals/VirtualSwitchCreateModalContainer.tsx";
import ConnectionInfoModalContainer from "../components/Modals/ConnectionInfoModalContainer.tsx";
import TerminalModalContainer from "../components/Modals/TerminalModalContainer.tsx";
import ErrorModalContainer from "../components/Modals/ErrorModalContainer.tsx";
import CapturedPacketDetailsModalContainer from "../components/CapturedPacketDetails/CapturedPacketDetailsModalContainer.tsx";

interface InterconnectModalsProviderProps {
    children: ReactNode;
}

const InterconnectModalsProvider = ({
    children,
}: InterconnectModalsProviderProps) => (
    <>
        <VirtualMachineCreateModalContainer />
        <VirtualSwitchCreateModalContainer />
        <ConnectionInfoModalContainer />
        <TerminalModalContainer />
        <ErrorModalContainer />
        <CapturedPacketDetailsModalContainer />
        {children}
    </>
);

export default InterconnectModalsProvider;
