import VirtualNetworkPlacingConnectionContainer from "../Entity/VirtualNetworkPlacingConnectionContainer.tsx";
import VirtualNetworkEntityRenderer from "./VirtualNetworkEntityRenderer.tsx";

const VirtualNetworkRenderer = () => {
    return <>
        <VirtualNetworkPlacingConnectionContainer />
        <VirtualNetworkEntityRenderer />
    </>;
};

export default VirtualNetworkRenderer;