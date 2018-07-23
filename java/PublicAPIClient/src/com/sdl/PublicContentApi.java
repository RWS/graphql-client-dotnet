package com.sdl;

import org.apache.http.client.HttpClient;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.HttpClients;

import java.io.IOException;
import java.io.InputStream;
import java.util.Map;

public class PublicContentApi {

    private IGraphQLClient _client;

    public PublicContentApi(IGraphQLClient graphQLClient) {
        _client = graphQLClient;
    }

    public static ItemConnection ExecuteItemQuery( InputItemFilter filter, IPagination pagination) {

        client.

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
}
