package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.junit.BeforeClass;
import org.junit.Test;

import java.io.*;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

public class DefaultGraphQLClientTest {

    private DefaultGraphQLClient client = null;
    private Properties prop =null;
    @BeforeClass
    public static void setUp() {

    }

    @Test
    public void executePublicationsQuery() throws IOException {
        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        String query = prop.getProperty("PUBLICATION_QUERY");
        String graphQLJsonResponse = client.execute(query, 0);
    }

    @Test
    public void executeItemTypesQuery() throws IOException {
        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");
        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        String query = prop.getProperty("ITEMTYPES_QUERY_AND_VARIABLES");
        String graphQLJsonResponse = client.execute(query, 0);

    }

    @Test
    public void executeItemTypesQueryUsingGraphQLRequest() throws IOException{

        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");
        prop.load(inputStream);

        String query = prop.getProperty("ITEMTYPES_QUERY");
        GraphQLRequest request = new GraphQLRequest();
        request.setQuery(query);

        String variables = prop.getProperty("ITEMTYPES_VARIABLES");
        HashMap<String,Object> variablesMap =
                new ObjectMapper().readValue("{"+variables+"}", HashMap.class);
        request.setVariables(variablesMap);

        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);
        String responsedata = client.execute(request);

    }

    @Test
    public void executePageItemQuery() throws IOException {
        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        PublicContentApi publicContentApi = new PublicContentApi(client);

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ContentComponent contentComponent = publicContentApi.ExecuteItemQuery(filter, pagination);
    }

    @Test
    public void executeSiteMap() throws IOException {
        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        Class<ContentQuery> dataModel = ContentQuery.class;
        PublicContentApi publicContentApi = new PublicContentApi(client);

        Page page=new Page();
        page.setNamespaceId(1);
        page.setPublicationId(5);
        page.setUrl("/index.html");

        publicContentApi.ExecuteSiteMap(page, dataModel);

    }

    @Test
    public void executeGetPageModelData() throws IOException {
        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        PublicContentApi publicContentApi = new PublicContentApi(client);

        publicContentApi.GetPageModelData(ContentNamespace.Sites, 7, 240, ContentType.MODEL, DataModelType.R2, PageInclusion.INCLUDE, true, null);

    }
}