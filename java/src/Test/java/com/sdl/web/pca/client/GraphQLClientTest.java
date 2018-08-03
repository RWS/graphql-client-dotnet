package com.sdl.web.pca.client;

import org.junit.BeforeClass;
import org.junit.Test;

import java.io.*;
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
        InputStream inputStream1 = GraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        InputStream inputStream = this.getClass().getResourceAsStream("testconfig.properties");
        prop.load(inputStream1);
        client = new GraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        String query = prop.getProperty("ITEMTYPES_QUERY");
        String graphQLJsonResponse = client.execute(query, 0);
    }
}