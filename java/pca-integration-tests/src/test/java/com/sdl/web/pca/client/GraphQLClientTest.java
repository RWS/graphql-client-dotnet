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

import java.io.InputStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

import static org.junit.Assert.assertNotNull;


public class GraphQLClientTest {

    private DefaultGraphQLClient client = null;
    private Properties prop = null;
    private PublicContentApi publicContentApi;

    @Before
    public void before() throws Exception {
        prop = new Properties();
        InputStream inputStream = GraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"), null);

        publicContentApi = new PublicContentApi(client);
    }

    @Test
    public void executePublicationsQuery() throws Exception {

        String query = prop.getProperty("PUBLICATION_QUERY");
        String graphQLJsonResponse = client.execute(query, 0);
        assertNotNull(graphQLJsonResponse);
    }

    @Test
    public void executeItemTypesQuery() throws Exception {

        String query = prop.getProperty("ITEMTYPES_QUERY_AND_VARIABLES");
        String graphQLJsonResponse = client.execute(query, 0);
        assertNotNull(graphQLJsonResponse);
    }

    @Test
    public void executeItemTypesQueryUsingGraphQLRequest() throws Exception {

        String query = prop.getProperty("ITEMTYPES_QUERY");
        GraphQLRequest request = new GraphQLRequest();
        request.setQuery(query);

        String variables = prop.getProperty("ITEMTYPES_VARIABLES");
        HashMap<String, Object> variablesMap =
                new ObjectMapper().readValue(variables, HashMap.class);
        request.setVariables(variablesMap);

        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"), null);
        String responsedata = client.execute(request);
        assertNotNull(responsedata);
    }

    @Test
    public void executePageItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection page = publicContentApi.executeItemQuery(filter, pagination, ContentQuery.class).getItems();
        assertNotNull(page);
    }

    @Test
    public void executeComponentItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection component = publicContentApi.executeItemQuery(filter, pagination, ContentQuery.class).getItems();
        assertNotNull(component);
    }

    @Test
    public void executeKeywordItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.KEYWORD));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection keyword = publicContentApi.executeItemQuery(filter, pagination, ContentQuery.class).getItems();
        assertNotNull(keyword);
    }

    @Test
    public void executePublicationItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PUBLICATION));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection publication = publicContentApi.executeItemQuery(filter, pagination, ContentQuery.class).getItems();
        assertNotNull(publication);
    }

    @Test
    public void executeSiteMap() throws Exception {

        Page page = new Page();
        page.setNamespaceId(1);
        page.setPublicationId(8);
        TaxonomySitemapItem taxonomySitemapItem = publicContentApi.executeSiteMap(page, ContentQuery.class).getSitemap();
        assertNotNull(taxonomySitemapItem);
    }

    @Test
    public void executeGetPageModelData() {
        assertNotNull(publicContentApi.getPageModelData(ContentNamespace.Sites, 8, 640, ContentType.MODEL, DataModelType.R2, PageInclusion.INCLUDE, true, null, ContentQuery.class));
    }

    @Test
    public void executeGetSitemap() {
        TaxonomySitemapItem taxonomySitemapItem = publicContentApi.getSitemap(ContentNamespace.Sites, 8, ContentQuery.class).getSitemap();
        assertNotNull(taxonomySitemapItem);
    }

    @Test
    public void executeGetSitemapSubtree() {
        TaxonomySitemapItem taxonomySitemapSubtreeItem = publicContentApi.getSitemapSubtree(ContentNamespace.Sites, 8, "t2680", true, ContentQuery.class).getSitemapSubtree();
        assertNotNull(taxonomySitemapSubtreeItem);
    }

    @Test
    public void executeGetEntityModelData() {
        assertNotNull(publicContentApi.getEntityModelData(ContentNamespace.Sites, 8, 1458, 9195, ContentQuery.class));
    }
}