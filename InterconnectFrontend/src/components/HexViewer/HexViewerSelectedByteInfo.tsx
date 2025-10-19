import { useTranslation } from "react-i18next";
import styles from "./HexViewerSelectedByteInfo.module.scss"

interface HexViewerSelectedByteInfoProps {
    binary: string;
    decimal: string;
}

const HexViewerSelectedByteInfo = ({
    binary,
    decimal,
}: HexViewerSelectedByteInfoProps) => {
    const { t } = useTranslation();

    return (
        <div className={styles["hex-viewer__selected-byte-info"]}>
            <p>
                {t("hexViewer.decimal")} {decimal}
            </p>
            <p>
                {t("hexViewer.binary")} {binary}
            </p>
        </div>
    );
};

export default HexViewerSelectedByteInfo;
