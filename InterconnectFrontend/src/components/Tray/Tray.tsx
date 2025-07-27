import { Box } from "@mantine/core";
import classes from "./Tray.module.scss";
import VirtualMachineCreateEntitiesTrayContainer from "./VirtualMachineCreateEntitiesTrayContainer.tsx";

const Tray = () => {
    return <Box className={classes["tray"]}>
        <VirtualMachineCreateEntitiesTrayContainer />
    </Box>;
};

export default Tray;