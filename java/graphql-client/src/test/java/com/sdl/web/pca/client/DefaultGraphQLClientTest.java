package com.sdl.web.pca.client;

import com.sdl.web.pca.client.exception.GraphQLClientException;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpStatus;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.CloseableHttpClient;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Answers;
import org.mockito.Mock;
import org.mockito.runners.MockitoJUnitRunner;

import java.io.IOException;
import java.util.HashMap;

import static org.junit.Assert.assertEquals;
import static org.mockito.Matchers.any;
import static org.mockito.Mockito.when;

@RunWith(MockitoJUnitRunner.class)
public class DefaultGraphQLClientTest {
    private static final String HOST = "http://localhost:8081/udp/content";
    private static final String REQUEST = "{\"query\":\"{items(filter:{namespaceIds:[1],itemTypes:[PUBLICATION]}," +
            "first:20){edges{node{id}}}}\"}";
    private static final String RESPONSE = "{\"data\":{\"items\":{\"edges\":[{\"node\":{\"id\":" +
            "\"dec06688-3c29-36e6-9f91-710c6109aab5\"}},{\"node\":{\"id\":\"8bc88c06-bd3c-331f-993d-eeafca5ab443\"}}," +
            "{\"node\":{\"id\":\"d77db3d1-593c-3aee-8370-b324c9f1ef8e\"}},{\"node\":{\"id\":" +
            "\"c39cf688-6160-3bfd-bad3-80ad65e8a600\"}},{\"node\":{\"id\":\"b0362468-33e0-3f21-b0d9-4d9466f94df3\"}}," +
            "{\"node\":{\"id\":\"3c0b637c-ccbd-3cfb-9c3c-b029032cdcb0\"}},{\"node\":{\"id\":" +
            "\"66958b1f-018c-3ee1-bb8f-3ca7f24d4dff\"}}]}}}";
    private static final int DEFAULT_TIMEOUT = 42;

    @Mock
    private CloseableHttpClient httpClient;
    @Mock(answer = Answers.RETURNS_DEEP_STUBS)
    private CloseableHttpResponse httpResponse;

    private DefaultGraphQLClient client;

    @Before
    public void setup() {
        client = new DefaultGraphQLClient(HOST, new HashMap<>(), null);
        client.setHttpClient(httpClient);
    }

    @Test
    public void executeWithTimeout() throws Exception {
        when(httpClient.execute(any(HttpUriRequest.class))).thenReturn(httpResponse);
        when(httpResponse.getEntity().getContent()).thenReturn(IOUtils
                .toInputStream(RESPONSE, "UTF-8"));
        when(httpResponse.getStatusLine().getStatusCode()).thenReturn(HttpStatus.SC_OK);

        String result = client.execute(REQUEST, DEFAULT_TIMEOUT);

        assertEquals(RESPONSE, result);
    }

    @Test
    public void testExecuteGraphQLRequest() throws Exception {
        when(httpClient.execute(any(HttpUriRequest.class))).thenReturn(httpResponse);
        when(httpResponse.getEntity().getContent()).thenReturn(IOUtils
                .toInputStream(RESPONSE, "UTF-8"));
        when(httpResponse.getStatusLine().getStatusCode()).thenReturn(HttpStatus.SC_OK);
        GraphQLRequest request = new GraphQLRequest(REQUEST, new HashMap<>(), DEFAULT_TIMEOUT);

        String result = client.execute(request);

        assertEquals(RESPONSE, result);
    }

    @Test(expected = GraphQLClientException.class)
    public void failedHttpClientRequest() throws Exception {
        when(httpClient.execute(any(HttpUriRequest.class))).thenThrow(IOException.class);

        client.execute(REQUEST);
    }

    @Test(expected = GraphQLClientException.class)
    public void incorrectHttpResponseCode() throws Exception {
        when(httpClient.execute(any(HttpUriRequest.class))).thenReturn(httpResponse);
        when(httpResponse.getEntity().getContent()).thenReturn(IOUtils
                .toInputStream("incorrect value", "UTF-8"));
        when(httpResponse.getStatusLine().getStatusCode()).thenReturn(HttpStatus.SC_BAD_REQUEST);

        client.execute(REQUEST, DEFAULT_TIMEOUT);
    }

}