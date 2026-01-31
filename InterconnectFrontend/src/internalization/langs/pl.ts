const pl = {
    errorOccurred: "Wystąpił błąd",
    connectingToServer: "Łączenie z serwerem...",
    terminal: "Terminal",
    network: "Sieć",
    slot: "Slot {{number}}",
    disconnectWith: "Rozłącz z {{entityName}}",
    connectWithAnotherEntity: "Połącz z innym urządzeniem",
    performingActions: "Wykonywanie akcji...",
    actions: "Akcje",
    type: "Typ",
    delete: "Usuń",
    virtualNetworkNode: {
        virtualNetworkNode: "Węzeł sieci",
        form: {
            newVirtualNetworkNode: "Nowy węzeł sieci",
            name: "Nazwa",
            createVirtualNetworkNode: "Stwórz węzeł sieci",
        },
    },
    virtualMachine: {
        virtualMachine: "Maszyna wirtualna",
        type: "Sposób prezentacji",
        configuration: "Konfiguracja maszyny wirtualnej",
        name: "Nazwa",
        cpu: "CPU",
        operationalMemory: "Pamięć operacyjna",
        bootableDisk: "Obraz systemu operacyjnego",
        form: {
            informationSection: "Informacje",
            resourcesSection: "Zasoby",
            createVirtualMachine: "Stwórz maszynę wirtualną",
            excessiveMemorySelectionWarning:
                "Chcesz zadeklarować więcej niż 40% dostępnej pamięci operacyjnej. Może to spowodować spowolnienie działania systemu gospodarza lub innych maszyn wirtualnych.",
            nameIsEmptyValidationError: "Nazwa nie może być pusta",
            virtualCpuNotSetValidationError:
                "Musisz wybrać przynajmniej jedno wirtualne CPU",
            memoryNotSetValidationError:
                "Musisz zadeklarować pamięć operacyjną dla maszyny wirtualnej",
            bootableDiskNotSelectedValidationError:
                "Musisz wybrać obraz dla maszyny wirtualnej",
        },
    },
    internet: {
        internet: "Internet",
    },
    packet: {
        sourceMac: "Źródłowy MAC",
        destinationMac: "Docelowy MAC",
        sourceIp: "Źródłowy IP",
        destinationIp: "Docelowy IP",
    },
    packetDetails: {
        name: "Nazwa",
        value: "Wartość",
        packetDetails: "Szczegóły pakietu",
        sourceMac: "Źródłowy adres MAC",
        destinationMac: "Docelowy adres MAC",
        sourceIp: "Źródłowy adres IP",
        destinationIp: "Docelowy adres IP",
        packetType: "Typ pakietu",
        ipVersion: "Wersja IP",
    },
    hexViewer: {
        decimal: "DEC: ",
        binary: "BIN: ",
    },
    virtualMachineEntityType: {
        host: "Host",
        server: "Serwer",
        router: "Router",
    },
};

export default pl;
