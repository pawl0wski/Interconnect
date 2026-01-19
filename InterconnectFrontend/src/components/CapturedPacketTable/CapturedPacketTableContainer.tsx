import CapturedPacketTable from "./CapturedPacketTable.tsx";
import { useEffect } from "react";
import useCapturedPacketStore from "../../store/capturedPacketStore.ts";

const CapturedPacketTableContainer = () => {
    const capturedPacketStore = useCapturedPacketStore();

    useEffect(() => {
        (async () => {
            await capturedPacketStore.startCapturingPackets();
        })();
    }, []);

    const capturedPackets = capturedPacketStore.capturedPackets.sort(
        (x, y) => y.timestampMicroseconds - x.timestampMicroseconds,
    );

    return <CapturedPacketTable packets={capturedPackets} />;
};

export default CapturedPacketTableContainer;
