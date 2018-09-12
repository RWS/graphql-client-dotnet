package com.sdl.web.pca.client.util;

import java.util.HashMap;
import java.util.Map;

public enum ItemTypes {
    NULL(0),
    IDNULL(-1),
    PUBLICATION(1),
    FOLDER(2),
    STRUCTURE_GROUP(4),
    SCHEMA(8),
    COMPONENT(16),
    COMPONENT_TEMPLATE(32),
    PAGE(64),
    PAGE_TEMPLATE(128),
    TARGET_GROUP(256),
    PUBLICATION_TARGET(512),
    KEYWORD(1024),
    COMPONENT_PRESENTATION(2048),
    TARGET_TYPE(33554432),
    TARGET_DESTINATION(67108864),
    TRANSACTION(66560);

    private int value;

    private static final Map<Integer, ItemTypes> itemTypeById = new HashMap<>();

    static {
        for (ItemTypes itemType : ItemTypes.values()) {
            itemTypeById.put(itemType.value, itemType);
        }
    }

    ItemTypes(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }

    public static ItemTypes getById(int id) {
        ItemTypes result = itemTypeById.get(id);
        if (result == null) {
            throw new IllegalArgumentException("Unable to resolve item type for Id " + id);
        }
        return result;
    }
}