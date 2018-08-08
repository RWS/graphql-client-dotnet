package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.request.IGraphQLRequest;
import org.junit.BeforeClass;
import org.junit.Test;

import java.io.*;
import java.util.HashMap;
import java.util.Properties;

public class GraphQLClientTest {

    private GraphQLClient client = null;
    private Properties prop =null;
    @BeforeClass
    public static void setUp() {

    }

    @Test
    public void executePublicationsQuery() throws IOException {
        prop = new Properties();
        InputStream inputStream = GraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new GraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        String query = prop.getProperty("PUBLICATION_QUERY");
        String graphQLJsonResponse = client.execute(query, 0);
    }

    @Test
    public void executeItemTypesQuery() throws IOException {
        prop = new Properties();
        InputStream inputStream = GraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");
        prop.load(inputStream);
        client = new GraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        String query = prop.getProperty("ITEMTYPES_QUERY_AND_VARIABLES");
        String graphQLJsonResponse = client.execute(query, 0);

    }

    @Test
    public void executeItemTypesQueryUsingGraphQLRequest() throws IOException{

        prop = new Properties();
        InputStream inputStream = GraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");
        prop.load(inputStream);

        String query = prop.getProperty("ITEMTYPES_QUERY");
        IGraphQLRequest request = new GraphQLRequest();
        request.setQuery(query);

        String variables = prop.getProperty("ITEMTYPES_VARIABLES");
        HashMap<String,Object> variablesMap =
                new ObjectMapper().readValue("{"+variables+"}", HashMap.class);
        request.setVariables(variablesMap);

        client = new GraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);
        String responsedata = client.execute(request);

    }
}