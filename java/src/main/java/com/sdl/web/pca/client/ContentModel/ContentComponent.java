package com.sdl.web.pca.client.contentmodel;

import java.util.List;

public class ContentComponent {

    private Data data;

    public void setData(Data data) {
        this.data = data;
    }

    public Data getData() {
        return data;
    }
}

class Data {

    private Items items;
    private Publication publication;
    private BinaryComponent binaryComponent;

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
}

class Items {

    private List<com.sdl.ContentModel.edges> edges;
    public void setEdges(List<com.sdl.ContentModel.edges> edges) {
        this.edges = edges;
    }
    public List<com.sdl.ContentModel.edges> getEdges() {
        return edges;
    }

}

class edges {

    private String cursor;
    private Node node;
    public void setCursor(String cursor) {
        this.cursor = cursor;
    }
    public String getCursor() {
        return cursor;
    }

    public void setNode(Node node) {
        this.node = node;
    }
    public Node getNode() {
        return node;
    }

}

class CustomMetas {

    private List<com.sdl.ContentModel.edges> edges;

    public void setEdges(List<com.sdl.ContentModel.edges> edges) {
        this.edges = edges;
    }

    public List<com.sdl.ContentModel.edges> getEdges() {
        return edges;
    }
}



