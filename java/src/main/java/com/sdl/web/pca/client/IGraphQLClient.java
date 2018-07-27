package com.sdl.web.pca.client;

public interface IGraphQLClient {
    int Timeout = 0;

     void setTimeout(int timeout);
     int getTimeout();
}
