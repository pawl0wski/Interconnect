import styles from "./HexViewer.module.scss";
import classNames from "classnames";

interface HexViewerContentBytesProps {
    hexContent: string[];
    selectedByteIndex: number | null;
    onByteMouseEnter: (index: number) => void;
    onByteMouseLeave: (index: number) => void;
}

const HexViewerContentBytes = ({
    hexContent,
    selectedByteIndex,
    onByteMouseEnter,
    onByteMouseLeave,
}: HexViewerContentBytesProps) => (
    <div className={styles["hex-viewer__content__bytes"]}>
        {hexContent.map((byte, i) => (
            <span
                key={`byte-${i}`}
                onMouseEnter={() => onByteMouseEnter(i)}
                onMouseLeave={() => onByteMouseLeave(i)}
                className={classNames({
                    [styles["selected"]]: selectedByteIndex === i,
                })}
            >
                {byte.toString()}
            </span>
        ))}
    </div>
);

export default HexViewerContentBytes;
