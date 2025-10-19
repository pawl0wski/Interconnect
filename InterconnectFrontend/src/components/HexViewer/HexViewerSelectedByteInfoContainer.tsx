import HexViewerSelectedByteInfo from "./HexViewerSelectedByteInfo.tsx";

interface HexViewerSelectedByteInfoContainerProps {
    selectedByte: number | null;
}

const HexViewerSelectedByteInfoContainer = ({
    selectedByte,
}: HexViewerSelectedByteInfoContainerProps) => {
    if (selectedByte === null) {
        return null;
    }

    const decimal = selectedByte.toString(10);
    const binary = selectedByte.toString(2);

    return <HexViewerSelectedByteInfo binary={binary} decimal={decimal} />;
};

export default HexViewerSelectedByteInfoContainer;
