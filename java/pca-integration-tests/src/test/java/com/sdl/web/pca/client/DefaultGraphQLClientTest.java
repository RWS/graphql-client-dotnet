package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.junit.After;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import java.io.*;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

import static org.junit.Assert.assertNull;

public class DefaultGraphQLClientTest {

    private DefaultGraphQLClient client = null;
    private Properties prop =null;
    private PublicContentApi publicContentApi;
    @BeforeClass
    public static void setUp() {

    }

    @Before
    public void before() throws IOException {

        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"),null);

        publicContentApi = new PublicContentApi(client);
    }

    @Test
    public void executePublicationsQuery() throws IOException {

        String query = prop.getProperty("PUBLICATION_QUERY");
        String graphQLJsonResponse = client.execute(query, 0);
    }


    @Test
    public void executeItemTypesQuery() throws IOException {

        String query = prop.getProperty("ITEMTYPES_QUERY_AND_VARIABLES");
        String graphQLJsonResponse = client.execute(query, 0);
    }

    @Test
    public void executeItemTypesQueryUsingGraphQLRequest() throws IOException{

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

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        publicContentApi.ExecuteItemQuery(filter, pagination);
    }

    @Test
    public void executeComponentItemQuery() throws IOException {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        publicContentApi.ExecuteItemQuery(filter, pagination);
    }

    @Test
    public void executeKeywordItemQuery() throws IOException {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.KEYWORD));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        publicContentApi.ExecuteItemQuery(filter, pagination);
    }

    @Test
    public void executePublicationItemQuery() throws IOException {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PUBLICATION));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        publicContentApi.ExecuteItemQuery(filter, pagination);
    }

    @Test
    public void executeSiteMap() throws IOException {
        Class<ContentQuery> dataModel = ContentQuery.class;

        Page page=new Page();
        page.setNamespaceId(1);
        page.setPublicationId(5);
        publicContentApi.ExecuteSiteMap(page, dataModel);

    }

    @Test
    public void executeGetPageModelData() throws IOException {
        publicContentApi.GetPageModelData(ContentNamespace.Sites, 7, 240, ContentType.MODEL, DataModelType.R2, PageInclusion.INCLUDE, true, null);
    }

    @Test
    public void executeGetSitemap() throws IOException {
        publicContentApi.GetSitemap(ContentNamespace.Sites, 7);
    }

    @Test
    public void executeGetSitemapSubtree(){
        publicContentApi.GetSitemapSubtree(ContentNamespace.Sites, 5, "t51-k320", true);
    }

    @Test
    public void executeGetEntityModelData(){
        publicContentApi.GetEntityModelData(ContentNamespace.Sites, 5,  1);
    }

    @After
    public void after()throws Exception{
        publicContentApi = null;
        assertNull(publicContentApi);
    }
}