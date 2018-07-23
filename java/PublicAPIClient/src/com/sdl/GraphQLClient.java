package com.sdl;

import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.HttpClients;

;

public class GraphQLClient implements IGraphQLClient {

    HttpClient httpclient;

    public GraphQLClient(String endpoint) {

        httpclient = HttpClients.createDefault();
        HttpPost httppost = new HttpPost(endpoint);
    }


}

