import "@fontsource/jetbrains-mono/400.css";
import { useState } from "react";
import HexViewer from "./HexViewer.tsx";
import HexViewerSelectedByteInfoContainer from "./HexViewerSelectedByteInfoContainer.tsx";

interface HexViewerContainerProps {
    bytes: Uint8Array;
}

const HexViewerContainer = ({ bytes }: HexViewerContainerProps) => {
    const [selectedByteIndex, setSelectedByteIndex] = useState<number | null>(
        null,
    );

    const hexContent = Array.from(bytes).map((b) =>
        b.toString(16).padStart(2, "0").toUpperCase(),
    );

    const asciiContent = Array.from(
        String.fromCharCode(...bytes).replace("\n", " "),
    );

    const onByteMouseEnter = (byteIndex: number) => {
        setSelectedByteIndex(byteIndex);
    };

    const onByteMouseLeave = (byteIndex: number) => {
        if (selectedByteIndex === byteIndex) {
            setSelectedByteIndex(null);
        }
    };

    const rows = Array.from({ length: hexContent.length / 8 }, (_, i) =>
        `${i}`.padStart(3, "0"),
    );

    return (
        <>
            <HexViewer
                hexContent={hexContent}
                asciiContent={asciiContent}
                rows={rows}
                selectedByteIndex={selectedByteIndex}
                onByteMouseEnter={onByteMouseEnter}
                onByteMouseLeave={onByteMouseLeave}
            />
            <HexViewerSelectedByteInfoContainer
                selectedByte={
                    selectedByteIndex !== null ? bytes[selectedByteIndex] : null
                }
            />
        </>
    );
};

export default HexViewerContainer;
