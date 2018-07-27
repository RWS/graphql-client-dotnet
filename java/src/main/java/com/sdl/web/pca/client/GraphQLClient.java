package com.sdl.web.pca.client;

import org.apache.commons.io.IOUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClients;

import java.io.IOException;
import java.io.InputStream;
import java.util.Map;

public class GraphQLClient implements IGraphQLClient {

    private int Timeout;

    public static String execute(String url, String jsonEntity, Map<String, String> headers) throws IOException {
        HttpClient httpclient = HttpClients.createDefault();
        HttpPost httppost = new HttpPost(url);
        for(Map.Entry<String, String> header : headers.entrySet()) {

            httppost.addHeader(header.getKey(), header.getValue());
        }

        StringEntity entity = new StringEntity(jsonEntity, ContentType.APPLICATION_JSON);

        httppost.setEntity(entity);

        //Execute and get the response.
        HttpResponse response = httpclient.execute(httppost);

        InputStream contentStream = response.getEntity().getContent();

        String contentString = IOUtils.toString(contentStream, "UTF-8");

        if(response.getStatusLine().getStatusCode() != 200) {
            throw new HttpResponseException(response.getStatusLine().getStatusCode(), "The server responded with" + contentString);
        }

        return contentString;
    }

    @Override
    public void setTimeout(int timeout) {
        Timeout = timeout;
    }
    @Override
    public int getTimeout() {
        return Timeout;
    }
}
