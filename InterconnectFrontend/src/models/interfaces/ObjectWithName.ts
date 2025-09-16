interface ObjectWithName {
    name: () => string | undefined;
    parent?: ObjectWithName;
}

export type { ObjectWithName };
