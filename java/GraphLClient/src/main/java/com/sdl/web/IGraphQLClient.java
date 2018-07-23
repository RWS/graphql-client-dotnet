package com.sdl.web;

public interface IGraphQLClient {
    int Timeout = 0;

     void setTimeout(int timeout);
     int getTimeout();
}
