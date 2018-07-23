package com.sdl;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.JsonObject;
import com.sdl.ContentModel.ContentComponent;

import java.io.IOException;
import java.net.URL;
import java.util.Map;

public class PublicContentApi implements IPublicContentApi{

    private URL endpoint;
    private Map<String, String> headers;

    public PublicContentApi(URL endpoint, Map<String, String> headers) {
        this.endpoint = endpoint;
        this.headers = headers;
    }

    public <T> T getComponent(String query, JsonObject variables) throws IOException, GraphQLException {
        JsonObject body = new JsonObject();
        body.addProperty("query", query);
        body.add( "variables", variables);

        String responseString = GraphQLClient.execute(endpoint.toString(), body.toString(), headers);

        ObjectMapper objectMapper = new ObjectMapper();
        ContentComponent contentComponent = objectMapper.readValue(responseString, ContentComponent.class);

        return (T) contentComponent;
    }
}
