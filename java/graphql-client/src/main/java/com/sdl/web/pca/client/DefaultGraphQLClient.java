package com.sdl.web.pca.client;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.sdl.web.pca.client.exceptions.GraphQLClientException;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.config.RequestConfig;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;

import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

public class DefaultGraphQLClient implements GraphQLClient {

    private CloseableHttpClient httpClient;
    private String endpoint;
    private Map<String, String> headers = new HashMap<>();

    public DefaultGraphQLClient(String endpoint, Map<String, String> headers) {
        this.httpClient = HttpClients.createDefault();
        this.endpoint = endpoint;
        this.headers.putAll(headers);
    }

    @Override
    public String execute(String jsonEntity) throws GraphQLClientException {
        return execute(jsonEntity, 0);
    }

    @Override
    public String execute(String jsonEntity, int timeout) throws GraphQLClientException {
        HttpPost httpPost = new HttpPost(endpoint);
        headers.forEach((key, value) -> httpPost.addHeader(key, value));

        if (timeout > 0) {
            RequestConfig params = RequestConfig.custom().setConnectTimeout(timeout).setSocketTimeout(timeout).build();
            httpPost.setConfig(params);
        }

        StringEntity entity = new StringEntity(jsonEntity, ContentType.APPLICATION_JSON);
        httpPost.setEntity(entity);
//TODO add logging
        //Execute and get the response.
        try (CloseableHttpResponse response = httpClient.execute(httpPost)) {
            InputStream contentStream = response.getEntity().getContent();

            String contentString = IOUtils.toString(contentStream, "UTF-8");

            if (response.getStatusLine().getStatusCode() != 200) {
                throw new GraphQLClientException("Response code is '" + response.getStatusLine().getStatusCode() +
                        "'. The response message: " + contentString);
            }
            return contentString;
        } catch (ClientProtocolException e) {
            throw new GraphQLClientException("Client Protocol Exception", e);
        } catch (IOException e) {
            throw new GraphQLClientException("IOException", e);
        }
    }

    @Override
    public String execute(GraphQLRequest request) throws GraphQLClientException {
        JsonObject body = new JsonObject();
        body.addProperty("query", request.getQuery());
        body.add("variables", new Gson().toJsonTree(request.getVariables()));

        return execute(body.toString(), request.getTimeout());

    }
}
