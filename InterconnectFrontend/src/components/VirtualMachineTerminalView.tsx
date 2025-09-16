import { Terminal } from "@xterm/xterm";
import { useCallback, useEffect, useRef } from "react";
import { virtualMachineConsoleHubClient } from "../api/hubClient/VirtualMachineConsoleHubClient.ts";
import TerminalDataResponse from "../api/responses/TerminalDataResponse.ts";
import { Base64 } from "js-base64";

interface TerminalProps {
    uuid: string;
}

const VirtualMachineTerminalView = ({ uuid }: TerminalProps) => {
    const terminalRef = useRef<HTMLDivElement>(null);
    const terminal = useRef<Terminal>(null);

    const handleTerminalOnKey = useCallback(
        (data: string) => {
            virtualMachineConsoleHubClient.sendDataToConsole(uuid, data);
        },
        [uuid],
    );

    const writeDataToTerminal = useCallback((base64Data: string) => {
        const byteTerminalData = Base64.decode(base64Data);
        terminal.current!.write(byteTerminalData);
    }, []);

    useEffect(() => {
        if (terminalRef.current) {
            terminal.current = new Terminal();
            terminal.current.open(terminalRef.current);
            terminal.current.onData(handleTerminalOnKey);
        }

        return () => {
            if (terminal.current) {
                terminal.current.dispose();
            }
        };
    }, [handleTerminalOnKey, uuid]);

    useEffect(() => {
        (async () => {
            const resp =
                await virtualMachineConsoleHubClient.getInitialDataForConsole(
                    uuid,
                );
            writeDataToTerminal(resp.data.data);
            await virtualMachineConsoleHubClient.joinConsoleGroup(uuid);
            virtualMachineConsoleHubClient.startListeningForNewTerminalData(
                (resp: TerminalDataResponse) => {
                    writeDataToTerminal(resp.data.data);
                },
            );
        })();

        return () => {
            virtualMachineConsoleHubClient.leaveConsoleGroup(uuid);
        };
    }, [uuid, writeDataToTerminal]);

    return (
        <div
            ref={terminalRef}
            style={{
                width: "100%",
                height: "100%",
            }}
        />
    );
};

export default VirtualMachineTerminalView;
