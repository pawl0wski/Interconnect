import VirtualNetworkPlacingEntityContainer from "../Entity/VirtualNetworkPlacingEntityContainer.tsx";
import VirtualNetworkEntityRenderer from "./VirtualNetworkEntityRenderer.tsx";

const VirtualNetworkRenderer = () => {
    return <>
        <VirtualNetworkPlacingEntityContainer />
        <VirtualNetworkEntityRenderer />
    </>;
};

export default VirtualNetworkRenderer;