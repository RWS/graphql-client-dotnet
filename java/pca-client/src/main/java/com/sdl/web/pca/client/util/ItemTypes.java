package com.sdl.web.pca.client.util;

public class ItemTypes {
    public static final int NULL = 0;
    public static final int IDNULL = -1;
    public static final int PUBLICATION = 1;
    public static final int FOLDER = 2;
    public static final int STRUCTURE_GROUP = 4;
    public static final int SCHEMA = 8;
    public static final int COMPONENT = 16;
    public static final int COMPONENT_TEMPLATE = 32;
    public static final int PAGE = 64;
    public static final int PAGE_TEMPLATE = 128;
    public static final int TARGET_GROUP = 256;
    public static final int PUBLICATION_TARGET = 512;
    public static final int KEYWORD = 1024;
    public static final int TARGET_TYPE = 33554432;
    public static final int TARGET_DESTINATION = 67108864;
    public static final int TRANSACTION = 66560;

    private ItemTypes() {
    }
}