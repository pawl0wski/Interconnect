import styles from "./HexViewer.module.scss";

interface HexViewerRowsNumerationProps {
    rows: string[];
}

const HexViewerRowsNumeration = ({ rows }: HexViewerRowsNumerationProps) => (
    <div className={styles["hex-viewer__content__rows-numeration"]}>
        {rows.map((x) => (
            <span>{x}</span>
        ))}
    </div>
);

export default HexViewerRowsNumeration;
