package com.sdl.web.pca.client;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.config.RequestConfig;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClients;

import java.io.IOException;
import java.io.InputStream;
import java.util.Map;

public class DefaultGraphQLClient implements GraphQLClient {

    private HttpClient httpClient;
    private HttpPost httpPost;

    public DefaultGraphQLClient(String endpoint, Map<String, String> headers) {
        httpClient = HttpClients.createDefault();
        httpPost = new HttpPost(endpoint);

        if (headers != null) {
            for (Map.Entry<String, String> header : headers.entrySet()) {
                httpPost.addHeader(header.getKey(), header.getValue());
            }
        }
    }

    @Override
    public String execute(String jsonEntity) throws IOException {
        return execute(jsonEntity, 0);
    }

    @Override
    public String execute(String jsonEntity, int timeout) throws IOException {
        if (timeout > 0) {
            final RequestConfig params = RequestConfig.custom().setConnectTimeout(timeout).setSocketTimeout(timeout).build();
            httpPost.setConfig(params);
        }

        StringEntity entity = new StringEntity(jsonEntity, ContentType.APPLICATION_JSON);
        httpPost.setEntity(entity);

        //Execute and get the response.
        HttpResponse response = httpClient.execute(httpPost);

        InputStream contentStream = response.getEntity().getContent();

        String contentString = IOUtils.toString(contentStream, "UTF-8");

        if (response.getStatusLine().getStatusCode() != 200) {
            throw new HttpResponseException(response.getStatusLine().getStatusCode(), "The server responded with" + contentString);
        }

        return contentString;
    }

    @Override
    public String execute(GraphQLRequest request) throws IOException {
        JsonObject body = new JsonObject();
        body.addProperty("query", request.getQuery());
        body.add("variables", new Gson().toJsonTree(request.getVariables()));
        String contentString = null;

        try {
            StringEntity entity = new StringEntity(body.toString(), ContentType.APPLICATION_JSON);
            httpPost.setEntity(entity);
            HttpResponse response = httpClient.execute(httpPost);
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
