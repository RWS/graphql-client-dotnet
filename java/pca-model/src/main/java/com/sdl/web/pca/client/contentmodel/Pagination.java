package com.sdl.web.pca.client.contentmodel;

public class Pagination implements IPagination {
    public int First;
    public String After;

    @Override
    public int getFirst() {
        return First;
    }

    @Override
    public void setFirst(int first) {
        First = first;
    }

    @Override
    public String getAfter() {
        return After;
    }

    @Override
    public void setAfter(String after) {
        After = after;
    }
}
