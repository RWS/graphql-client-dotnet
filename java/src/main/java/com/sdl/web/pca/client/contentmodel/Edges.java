package com.sdl.web.pca.client.contentmodel;

public class Edges {

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
