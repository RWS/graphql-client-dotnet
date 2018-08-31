package com.sdl.web.pca.client.contentmodel;

public enum ContentNamespace {
    Sites(1), Docs(2);

    public int namespaceIds;

    ContentNamespace(int namespaceIds){
        this.namespaceIds = namespaceIds;
    }

    public int getNameSpaceValue(){
        return namespaceIds;
    }
}
