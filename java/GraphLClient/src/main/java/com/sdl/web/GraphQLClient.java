package com.sdl.web;

public class GraphQLClient implements IGraphQLClient{

    private int Timeout;

    @Override
    public void setTimeout(int timeout) {
        Timeout = timeout;
    }
    @Override
    public int getTimeout() {
        return Timeout;
    }
}
