package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.generated.InputClaimValue;
import com.sdl.web.pca.client.contentmodel.generated.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.generated.ItemConnection;
import com.sdl.web.pca.client.contentmodel.generated.ItemType;
import com.sdl.web.pca.client.contentmodel.generated.PublicationMapping;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.util.CmUri;
import org.junit.Before;
import org.junit.Test;

import java.io.InputStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

import static org.junit.Assert.assertNotNull;

public class GraphQLClientTest {

    private DefaultGraphQLClient client = null;
    private Properties prop = null;
    private DefaultPublicContentApi publicContentApi;

    @Before
    public void before() throws Exception {
        prop = new Properties();
        InputStream inputStream = GraphQLClientTest.class.getClassLoader()
                .getResourceAsStream("testconfig.properties");

        prop.load(inputStream);
        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"), null);

        publicContentApi = new DefaultPublicContentApi(client);
    }

    @Test
    public void getBinaryComponentById() throws Exception {
        Object result = publicContentApi.getBinaryComponent(ContentNamespace.Sites, 8, 756,
                null);
        assertNotNull(result);
    }

    @Test
    public void getBinaryComponentByUrl() throws Exception {
        Object result = publicContentApi.getBinaryComponent(ContentNamespace.Sites, 8,
                "/media/balloons_tcm8-756.jpg", null);
        assertNotNull(result);
    }

    @Test
    public void getBinaryCompoonentByCmUri() throws Exception {
        Object result = publicContentApi.getBinaryComponent(new CmUri("tcm:8-756-16"), new ContextData());
        assertNotNull(result);
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

        String variables = prop.getProperty("ITEMTYPES_VARIABLES");
        HashMap<String, Object> variablesMap =
                new ObjectMapper().readValue(variables, HashMap.class);
        GraphQLRequest request = new GraphQLRequest(query, variablesMap);

        client = new DefaultGraphQLClient(prop.getProperty("GRAPHQL_SERVER_ENDPOINT"), null);
        String responsedata = client.execute(request);
        assertNotNull(responsedata);
    }

    @Test
    public void executePageItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection contentQuery = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);
        assertNotNull(contentQuery);
    }

    @Test
    public void executeComponentItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection contentQuery = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);
        assertNotNull(contentQuery);
    }

    @Test
    public void executeKeywordItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.KEYWORD));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection contentQuery = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);
    }

    @Test
    public void executePublicationItemQuery() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.PUBLICATION));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);

        ItemConnection contentQuery = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);
    }

    @Test
    public void executeGetPageModelDataById() {
        assertNotNull(publicContentApi.getPageModelData(ContentNamespace.Sites, 1082, 640,
                ContentType.MODEL, DataModelType.DD4T, PageInclusion.INCLUDE, false, new ContextData()));
    }

    @Test
    public void executeGetPageModelDataByUri() {
        assertNotNull(publicContentApi.getPageModelData(ContentNamespace.Sites, 1082, "/example-legacy/index.html",
                ContentType.MODEL, DataModelType.R2, PageInclusion.INCLUDE, false, new ContextData()));
    }

    @Test
    public void executeGetEntityModelData() {
        assertNotNull(publicContentApi.getEntityModelData(ContentNamespace.Sites, 8, 1458, 9195,
                ContentType.MODEL, DataModelType.R2, DcpType.DEFAULT, false, new ContextData()));
    }

    @Test
    public void executeGetSitemap() {
        assertNotNull(publicContentApi.getSitemap(ContentNamespace.Sites, 8, 2,
                new ContextData()));
    }

    @Test
    public void executeGetSitemapSubtree() {
        assertNotNull(publicContentApi.getSitemapSubtree(ContentNamespace.Sites, 8, "t2680-k10019",
                2, true, new ContextData()));
    }

    @Test
    public void executeResolveBinaryLink() {
        String result = publicContentApi.resolveBinaryLink(ContentNamespace.Sites, 8, 756, "[#def#]");
        assertNotNull(result);
    }

    @Test
    public void executeResolvePageLink() {
        String result = publicContentApi.resolvePageLink(ContentNamespace.Sites, 8, 4447);
        assertNotNull(result);
    }

    @Test
    public void executeResolveComponentLink() {
        String result = publicContentApi.resolveComponentLink(ContentNamespace.Sites, 8, 3286,640,3292);
        assertNotNull(result);
    }

    @Test
    public void executeResolveDynamicComponentLink() {
        String result = publicContentApi.resolveDynamicComponentLink(ContentNamespace.Sites, 1082, 4569,4565,9195);
        assertNotNull(result);
    }

    @Test
    public void executegetPulbicationMapping() {
        PublicationMapping result = publicContentApi.getPublicationMapping(ContentNamespace.Sites, "http://localhost:8882/");
        assertNotNull(result);
    }

    @Test
    public void executeGetPublication() {
        assertNotNull(publicContentApi.getPublication(ContentNamespace.Sites, 8, new ContextData(), ""));
    }

    @Test
    public void executeGetPublications(){
        Pagination pagination = new Pagination();
        pagination.setFirst(1);
        assertNotNull(publicContentApi.getPublications(ContentNamespace.Sites, pagination, null, new ContextData(), ""));
    }
}