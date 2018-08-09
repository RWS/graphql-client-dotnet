package com.sdl.web.pca.client;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.request.IGraphQLRequest;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.config.RequestConfig;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClients;
import org.json.simple.JSONObject;

import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

public class GraphQLClient implements IGraphQLClient {

    private HttpClient _httpClient;
    private HttpPost _httpPost;

    public GraphQLClient(String endpoint, Map<String,String> headers)
    {
        _httpClient = HttpClients.createDefault();
        _httpPost = new HttpPost(endpoint);

        if (headers != null) {
            for (Map.Entry<String, String> header : headers.entrySet()) {
                _httpPost.addHeader(header.getKey(), header.getValue());
            }
        }
    }

    @Override
    public String execute(String jsonEntity) throws IOException {
        return execute(jsonEntity, 0);
    }

    @Override
    public String execute(String jsonEntity, int timeout) throws IOException {
        if(timeout>0) {
            final RequestConfig params = RequestConfig.custom().setConnectTimeout(timeout).setSocketTimeout(timeout).build();
            _httpPost.setConfig(params);
        }

        StringEntity entity = new StringEntity(jsonEntity, ContentType.APPLICATION_JSON);
        _httpPost.setEntity(entity);

        //Execute and get the response.
        HttpResponse response = _httpClient.execute(_httpPost);

        InputStream contentStream = response.getEntity().getContent();

        String contentString = IOUtils.toString(contentStream, "UTF-8");

        if(response.getStatusLine().getStatusCode() != 200) {
            throw new HttpResponseException(response.getStatusLine().getStatusCode(), "The server responded with" + contentString);
        }

        return contentString;
    }

    @Override
    public String execute(IGraphQLRequest request) throws IOException {
        JsonObject body = new JsonObject();
        body.addProperty("query", request.getQuery());
        body.add("variables", new Gson().toJsonTree(request.getVariables()));
        String contentString = null;

        try {
            StringEntity entity = new StringEntity(body.toString(), ContentType.APPLICATION_JSON);
            _httpPost.setEntity(entity);
            HttpResponse response = _httpClient.execute(_httpPost);
            InputStream contentStream = response.getEntity().getContent();

            contentString = IOUtils.toString(contentStream, "UTF-8");

            if (response.getStatusLine().getStatusCode() != 200) {
                throw new HttpResponseException(response.getStatusLine().getStatusCode(), "The server responded with" + contentString);
            }
        }catch (Exception ex){ex.printStackTrace();}
        return contentString;
    }
}
