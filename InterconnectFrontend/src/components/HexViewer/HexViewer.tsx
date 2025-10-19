import styles from "./HexViewer.module.scss";
import HexViewerHeader from "./HexViewerHeader.tsx";
import HexViewerRowsNumeration from "./HexViewerRowsNumeration.tsx";
import HexViewerContentBytes from "./HexViewerContentBytes.tsx";
import HexViewerContentAscii from "./HexViewerContentAscii.tsx";

interface HexViewerProps {
    asciiContent: string[];
    hexContent: string[];
    rows: string[];
    selectedByteIndex: number | null;
    onByteMouseEnter: (byteIndex: number) => void;
    onByteMouseLeave: (byteIndex: number) => void;
}

const HexViewer = ({
    hexContent,
    asciiContent,
    rows,
    selectedByteIndex,
    onByteMouseEnter,
    onByteMouseLeave,
}: HexViewerProps) => {
    return (
        <div className={styles["hex-viewer"]}>
            <HexViewerHeader />
            <div className={styles["hex-viewer__rows"]}>
                <HexViewerRowsNumeration rows={rows} />
                <div className={styles["hex-viewer__content"]}>
                    <HexViewerContentBytes
                        hexContent={hexContent}
                        selectedByteIndex={selectedByteIndex}
                        onByteMouseEnter={onByteMouseEnter}
                        onByteMouseLeave={onByteMouseLeave}
                    />
                    <HexViewerContentAscii
                        asciiContent={asciiContent}
                        selectedByteIndex={selectedByteIndex}
                    />
                </div>
            </div>
        </div>
    );
};

export default HexViewer;
