import classes from "./InputWarning.module.scss";
import { MdWarning } from "react-icons/md";

interface InputWarningProps {
    text: string;
}

const InputWarning = ({ text }: InputWarningProps) => {
    return (
        <div className={classes["input-warning"]}>
            <div className={classes["input-warning__icon"]}>
                <MdWarning size={20} />
            </div>
            <div className={classes["input-warning__text"]}>{text}</div>
        </div>
    );
};

export default InputWarning;
