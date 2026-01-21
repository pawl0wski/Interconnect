import { ReactNode } from "react";
import VirtualMachineCreateModalContainer from "../components/Modals/VirtualMachineCreateModalContainer.tsx";
import VirtualNetworkNodeCreateModalContainer from "../components/Modals/VirtualNetworkNodeCreateModalContainer.tsx";
import ConnectionInfoModalContainer from "../components/Modals/ConnectionInfoModalContainer.tsx";
import TerminalModalContainer from "../components/Modals/TerminalModalContainer.tsx";
import ErrorModalContainer from "../components/Modals/ErrorModalContainer.tsx";
import CapturedPacketDetailsModalContainer from "../components/CapturedPacketDetails/CapturedPacketDetailsModalContainer.tsx";

interface InterconnectModalsProviderProps {
    children: ReactNode;
}

/**
 * Provider component that renders all application modals and dialogs.
 * Includes modals for entity creation, connection info, terminal access, error messages, and packet details.
 * @param {InterconnectModalsProviderProps} props - Component props
 * @param {ReactNode} props.children - Child components to render before the modals
 * @returns {JSX.Element} All modal containers with children
 */
const InterconnectModalsProvider = ({
    children,
}: InterconnectModalsProviderProps) => (
    <>
        <VirtualMachineCreateModalContainer />
        <VirtualNetworkNodeCreateModalContainer />
        <ConnectionInfoModalContainer />
        <TerminalModalContainer />
        <ErrorModalContainer />
        <CapturedPacketDetailsModalContainer />
        {children}
    </>
);

export default InterconnectModalsProvider;
