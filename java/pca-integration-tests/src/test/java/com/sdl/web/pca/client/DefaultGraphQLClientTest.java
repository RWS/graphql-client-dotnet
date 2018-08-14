package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.pagemodeldata.Sitemapkeyword;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.junit.After;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import java.io.InputStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

import static org.junit.Assert.assertNull;

public class DefaultGraphQLClientTest {

    private DefaultGraphQLClient client = null;
    private Properties prop = null;
    private PublicContentApi publicContentApi;

    @BeforeClass
    public static void setUp() {

    }

    @Before
    public void before() throws Exception {

        prop = new Properties();
        InputStream inputStream = DefaultGraphQLClientTest.class.getClassLoader().getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"), null);

        publicContentApi = new PublicContentApi(client);
    }

    @Test
    public void executePublicationsQuery() throws Exception {

        String query = prop.getProperty("PUBLICATION_QUERY");
        String graphQLJsonResponse = client.execute(query, 0);
    }


    @Test
    public void executeItemTypesQuery() throws Exception {

        String query = prop.getProperty("ITEMTYPES_QUERY_AND_VARIABLES");
        String graphQLJsonResponse = client.execute(query, 0);
    }

    @Test
    public void executeItemTypesQueryUsingGraphQLRequest() throws Exception {

        String query = prop.getProperty("ITEMTYPES_QUERY");
        GraphQLRequest request = new GraphQLRequest();
        request.setQuery(query);

        String variables = prop.getProperty("ITEMTYPES_VARIABLES");
        HashMap<String, Object> variablesMap =
                new ObjectMapper().readValue("{" + variables + "}", HashMap.class);
        request.setVariables(variablesMap);

        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"), null);
        String responsedata = client.execute(request);

    }

    @Test
    public void executePageItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        IContentComponent contentComponent = publicContentApi.executeItemQuery(filter, pagination, IContentComponent.class);
    }

    @Test
    public void executeComponentItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        IContentComponent contentComponent = publicContentApi.executeItemQuery(filter, pagination, IContentComponent.class);
    }

    @Test
    public void executeKeywordItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.KEYWORD));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        IContentComponent contentComponent = publicContentApi.executeItemQuery(filter, pagination, IContentComponent.class);
    }

    @Test
    public void executePublicationItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PUBLICATION));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        IContentComponent contentComponent = publicContentApi.executeItemQuery(filter, pagination, IContentComponent.class);
    }

    @Test
    public void executeSiteMap() throws Exception {
        Class<ContentQuery> dataModel = ContentQuery.class;

        Page page = new Page();
        page.setNamespaceId(1);
        page.setPublicationId(5);
        publicContentApi.executeSiteMap(page, dataModel);

    }

    @Test
    public void executeGetPageModelData() {
        publicContentApi.getPageModelData(ContentNamespace.Sites, 7, 240, ContentType.MODEL, DataModelType.R2, PageInclusion.INCLUDE, true, null, Page.class);
    }

    @Test
    public void executeGetSitemap() {
        publicContentApi.getSitemap(ContentNamespace.Sites, 7, Sitemapkeyword.class);
    }

    @Test
    public void executeGetSitemapSubtree() {
        publicContentApi.getSitemapSubtree(ContentNamespace.Sites, 5, "t51-k320", true, Sitemapkeyword.class);
    }

    @Test
    public void executeGetEntityModelData() {
        publicContentApi.getEntityModelData(ContentNamespace.Sites, 5, 1, IItem.class);
    }

    @After
    public void after() {
        publicContentApi = null;
        assertNull(publicContentApi);
    }
}