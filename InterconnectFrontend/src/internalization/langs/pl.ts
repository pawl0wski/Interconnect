const pl = {
    errorOccurred: "Wystąpił błąd",
    connectingToServer: "Łączenie z serwerem...",
    terminal: "Terminal",
    network: "Sieć",
    slot: "Slot {{number}}",
    disconnectWith: "Rozłącz z {{entityName}}",
    connectWithAnotherEntity: "Połącz z innym urządzeniem",
    virtualSwitch: {
        virtualSwitch: "Switch",
        form: {
            newVirtualSwitch: "Nowy switch wirtualny",
            name: "Nazwa",
            createVirtualSwitch: "Stwórz wirtualny switch",
        },
    },
    virtualMachine: {
        virtualMachine: "Maszyna wirtualna",
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
};

export default pl;
