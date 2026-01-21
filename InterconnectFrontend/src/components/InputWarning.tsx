import classes from "./InputWarning.module.scss";
import { MdWarning } from "react-icons/md";

/**
 * Props for the `InputWarning` component.
 */
interface InputWarningProps {
    text: string;
}

/**
 * Small helper component that displays a warning icon and message
 * inline near an input control.
 * @param props Component props
 * @param props.text Warning message to display
 * @returns A styled warning row with icon and text
 */
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
