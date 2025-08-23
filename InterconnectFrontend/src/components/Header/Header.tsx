import logo from "../../static/logo.svg";
import { Flex } from "@mantine/core";
import styles from "./Header.module.scss";

const Header = () => {
    return <Flex mx={10} h="100%" align="center">
        <img className={styles.logo} src={logo} alt="Interconnect logo" />
    </Flex>;
};

export default Header;