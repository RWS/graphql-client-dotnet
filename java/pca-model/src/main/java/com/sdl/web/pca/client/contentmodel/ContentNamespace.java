package com.sdl.web.pca.client.contentmodel;

public enum ContentNamespace {
    Sites(1), Docs(2);

    private int nameSpaceValue;

    ContentNamespace(int nameSpaceValue){
        this.nameSpaceValue = nameSpaceValue;
    }

    public int getNameSpaceValue(){
        return nameSpaceValue;
    }
}
