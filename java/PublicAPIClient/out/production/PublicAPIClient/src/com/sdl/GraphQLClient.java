package com.sdl;

import com.google.gson.Gson;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.StringEntity;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.JsonObject;
import org.apache.http.impl.client.HttpClients;


import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

public class GraphQLClient implements IGraphQLClient {

    HttpClient httpclient;
    HttpPost httppost;
    String endpoint;

    public GraphQLClient(String endpoint) {
        this.endpoint = endpoint;
    }

    @Override
    public String Execute(IGraphQLRequest graphQLRequest) throws IOException {

        JsonObject body = new JsonObject();
        body.addProperty("query", graphQLRequest.getQuery());
        body.add("variables", new Gson().toJsonTree(graphQLRequest.getVariables()));

        String contentString = null;
        httpclient = HttpClients.createDefault();
        httppost = new HttpPost(endpoint);

        try {
                StringEntity entity = new StringEntity(body.toString(), ContentType.APPLICATION_JSON);
                httppost.setEntity(entity);
                HttpResponse response = httpclient.execute(httppost);
                InputStream contentStream = response.getEntity().getContent();
                contentString = IOUtils.toString(contentStream, "UTF-8");

                if (response.getStatusLine().getStatusCode() != 200) {
                    throw new HttpResponseException(response.getStatusLine().getStatusCode(), "The server responded with" + contentString);
                }
            } catch (Exception ex) {
                ex.printStackTrace();
            }

            return contentString;

    }

}
