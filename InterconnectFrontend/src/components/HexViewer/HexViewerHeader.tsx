import styles from "./HexViewer.module.scss";

const HexViewerHeader = () => (
    <div className={styles["hex-viewer__header"]}>
        <span>00</span>
        <span>01</span>
        <span>02</span>
        <span>03</span>
        <span>04</span>
        <span>05</span>
        <span>06</span>
        <span>07</span>
        <span>08</span>
    </div>
);

export default HexViewerHeader;
