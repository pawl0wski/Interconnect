import styles from "./HexViewer.module.scss";
import classNames from "classnames";

interface HexViewerContentAsciiProps {
    asciiContent: string[];
    selectedByteIndex: number | null;
}

const HexViewerContentAscii = ({
    asciiContent,
    selectedByteIndex,
}: HexViewerContentAsciiProps) => (
    <div className={styles["hex-viewer__content__ascii"]}>
        {asciiContent.map((char, i) => (
            <span
                key={`char-${i}`}
                className={classNames({
                    [styles["selected"]]: selectedByteIndex === i,
                })}
            >
                {char}
            </span>
        ))}
    </div>
);

export default HexViewerContentAscii;
