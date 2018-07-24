package com.sdl;

import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;
import java.util.HashMap;


public class PublicContentApi {

    public IGraphQLClient _client;

    public PublicContentApi(IGraphQLClient graphQLClient) {
        _client = graphQLClient;
    }

    public <T> T ExecuteItemQuery( InputItemFilter filter, IPagination pagination) throws IOException {

        GraphQLQueries graphQLQueries = new GraphQLQueries();
        String query = graphQLQueries.Load(filter, pagination);
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        HashMap<String, Object> dictionary = new HashMap<String, Object>();
        dictionary.put("first", pagination.getFirst());
        dictionary.put("after", pagination.getAfter());
        dictionary.put("filter", filter);
        dictionary.put("contextData", inputClaimValues);

        IGraphQLRequest graphQLRequest =new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(dictionary);

        String contentQuery = _client.Execute(graphQLRequest);

        ObjectMapper objectMapper = new ObjectMapper();

        ContentComponent contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);


       return (T) contentComponent;
    }
}
