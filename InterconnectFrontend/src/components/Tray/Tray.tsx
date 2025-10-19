import { Flex } from "@mantine/core";
import classes from "./Tray.module.scss";
import VirtualMachineCreateEntityTrayButtonContainer from "./Buttons/VirtualMachineCreateEntityTrayButtonContainer.tsx";
import VirtualNetworkNodeCreateEntityTrayButtonContainer from "./Buttons/VirtualNetworkNodeCreateEntityTrayButtonContainer.tsx";
import InternetEntityTrayButtonContainer from "./Buttons/InternetEntityTrayButtonContainer.tsx";

const Tray = () => {
    return (
        <Flex className={classes["tray"]} mx="lg" justify="start" gap="lg">
            <VirtualMachineCreateEntityTrayButtonContainer />
            <VirtualNetworkNodeCreateEntityTrayButtonContainer />
            <InternetEntityTrayButtonContainer />
        </Flex>
    );
};

export default Tray;
