import { useEffect } from "react";
import { useInternetEntitiesStore } from "../../../store/entitiesStore.ts";
import InternetEntityContainer from "../Entity/InternetEntityContainer.tsx";

const InternetEntityRenderer = () => {
    const internetEntitiesStore = useInternetEntitiesStore();

    useEffect(() => {
        (async () => {
            await internetEntitiesStore.fetchEntities();
        })();
    }, []);

    return internetEntitiesStore.entities.map((e) => (
        <InternetEntityContainer key={e.id} entity={e} />
    ));
};

export default InternetEntityRenderer;
