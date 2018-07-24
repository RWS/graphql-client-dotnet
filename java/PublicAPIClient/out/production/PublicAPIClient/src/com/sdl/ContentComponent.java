package com.sdl;

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
    private ItemConnection itemConnection;

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

    public ItemConnection getItemConnection() {
        return itemConnection;
    }

    public void setItemConnection(ItemConnection itemConnection) {
        this.itemConnection = itemConnection;
    }
}

class Items {

    private List<com.sdl.edges> edges;
    public void setEdges(List<com.sdl.edges> edges) {
        this.edges = edges;
    }
    public List<com.sdl.edges> getEdges() {
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

    private List<com.sdl.edges> edges;

    public void setEdges(List<com.sdl.edges> edges) {
        this.edges = edges;
    }

    public List<com.sdl.edges> getEdges() {
        return edges;
    }
}



