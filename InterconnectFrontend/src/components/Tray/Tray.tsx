import { Flex } from "@mantine/core";
import classes from "./Tray.module.scss";
import VirtualMachineCreateEntityTrayButtonContainer from "./Buttons/VirtualMachineCreateEntityTrayButtonContainer.tsx";
import VirtualSwitchCreateEntityTrayButtonContainer from "./Buttons/VirtualSwitchCreateEntityTrayButtonContainer.tsx";

const Tray = () => {
    return <Flex className={classes["tray"]} mx="lg" justify="start" gap="lg">
        <VirtualMachineCreateEntityTrayButtonContainer />
        <VirtualSwitchCreateEntityTrayButtonContainer />
    </Flex>;
};

export default Tray;