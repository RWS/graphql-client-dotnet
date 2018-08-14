package com.sdl.web.pca.client.contentmodel;

class Data {

    private Items items;
    private Publication publication;
    private BinaryComponent binaryComponent;
    private Page page;

    public void setItems(Items items) {
        this.items = items;
    }
    public Items getItems() {
        return items;
    }

    public void setPublication(Publication publication) {
        this.publication = publication;
    }
    public Publication getPublication() {
        return publication;
    }

    public void setBinaryComponent(BinaryComponent binaryComponent) {
        this.binaryComponent = binaryComponent;
    }
    public BinaryComponent getBinaryComponent() {
        return binaryComponent;
    }

    public Page getPage() {
        return page;
    }
    public void setPage(Page page) {
        this.page = page;
    }
}
