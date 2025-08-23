import { Box } from "@mantine/core";
import classes from "./Tray.module.scss";
import VirtualMachineCreateEntityTrayButtonContainer
    from "./Buttons/VirtualMachineCreateEntityTrayButtonContainer.tsx";

const Tray = () => {
    return <Box className={classes["tray"]} mx="lg">
        <VirtualMachineCreateEntityTrayButtonContainer />
    </Box>;
};

export default Tray;