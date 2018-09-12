package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.generated.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.generated.Component;
import com.sdl.web.pca.client.contentmodel.generated.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.generated.ItemConnection;
import com.sdl.web.pca.client.contentmodel.generated.ItemType;
import com.sdl.web.pca.client.contentmodel.generated.Keyword;
import com.sdl.web.pca.client.contentmodel.generated.Page;
import com.sdl.web.pca.client.contentmodel.generated.Publication;
import com.sdl.web.pca.client.contentmodel.generated.PublicationConnection;
import com.sdl.web.pca.client.contentmodel.generated.PublicationMapping;
import com.sdl.web.pca.client.contentmodel.generated.TaxonomySitemapItem;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.util.CmUri;
import com.sdl.web.pca.client.util.ItemTypes;
import org.junit.Before;
import org.junit.Test;

import java.io.InputStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

import static org.junit.Assert.assertEquals;
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
        BinaryComponent result = publicContentApi.getBinaryComponent(ContentNamespace.Sites, 8, 756,
                null);

        assertEquals(BinaryComponent.class, result.getClass());
        assertEquals("b4e5c7c4-f04a-3d6d-898f-5886d0f648bd", result.getId());
        assertEquals(1, result.getNamespaceId());
        assertEquals(8, result.getPublicationId());
        assertEquals("tcd:pub[8]/componentmeta[756]", result.getTitle());
        assertEquals(756, result.getVariants().getEdges().get(0).getNode().getBinaryId());
        assertEquals("/media/balloons_tcm8-756.jpg", result.getVariants().getEdges().get(0).getNode().getPath());
        assertEquals("/media/balloons_tcm8-756.jpg", result.getVariants().getEdges().get(0).getNode().getUrl());
        assertEquals("http://localhost:8081/udp/content/binary/1/8/756", result.getVariants().getEdges().get(0).getNode().getDownloadUrl());
        assertEquals("[#def#]", result.getVariants().getEdges().get(0).getNode().getVariantId());

    }

    @Test
    public void getBinaryComponentByUrl() throws Exception {
        BinaryComponent result = publicContentApi.getBinaryComponent(ContentNamespace.Sites, 8,
                "/media/balloons_tcm8-756.jpg", null);

        assertEquals(BinaryComponent.class, result.getClass());
        assertEquals("b4e5c7c4-f04a-3d6d-898f-5886d0f648bd", result.getId());
        assertEquals(1, result.getNamespaceId());
        assertEquals(8, result.getPublicationId());
        assertEquals("tcd:pub[8]/componentmeta[756]", result.getTitle());
        assertEquals(756, result.getVariants().getEdges().get(0).getNode().getBinaryId());
        assertEquals("/media/balloons_tcm8-756.jpg", result.getVariants().getEdges().get(0).getNode().getPath());
        assertEquals("/media/balloons_tcm8-756.jpg", result.getVariants().getEdges().get(0).getNode().getUrl());
        assertEquals("http://localhost:8081/udp/content/binary/1/8/756", result.getVariants().getEdges().get(0).getNode().getDownloadUrl());
        assertEquals("[#def#]", result.getVariants().getEdges().get(0).getNode().getVariantId());

    }

    @Test
    public void getBinaryCompoonentByCmUri() throws Exception {
        BinaryComponent result = publicContentApi.getBinaryComponent(new CmUri("tcm:8-756-16"), new ContextData());

        assertEquals(BinaryComponent.class, result.getClass());
        assertEquals("b4e5c7c4-f04a-3d6d-898f-5886d0f648bd", result.getId());
        assertEquals(756, result.getItemId());
        assertEquals(1, result.getNamespaceId());
        assertEquals(8, result.getPublicationId());
        assertEquals("tcd:pub[8]/componentmeta[756]", result.getTitle());
        assertEquals(756, result.getVariants().getEdges().get(0).getNode().getBinaryId());
        assertEquals("/media/balloons_tcm8-756.jpg", result.getVariants().getEdges().get(0).getNode().getPath());
        assertEquals("/media/balloons_tcm8-756.jpg", result.getVariants().getEdges().get(0).getNode().getUrl());
        assertEquals("http://localhost:8081/udp/content/binary/1/8/756", result.getVariants().getEdges().get(0).getNode().getDownloadUrl());
        assertEquals("[#def#]", result.getVariants().getEdges().get(0).getNode().getVariantId());
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
    public void executeItemQueryPage() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        ItemConnection result = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);
        assertEquals(10, result.getEdges().size());
        assertEquals("MQ==", result.getEdges().get(0).getCursor());
        assertEquals(Page.class, result.getEdges().get(0).getNode().getClass());
        assertEquals("Publish Settings", result.getEdges().get(0).getNode().getTitle());
        assertEquals(ItemTypes.PAGE.getValue(), result.getEdges().get(0).getNode().getItemType());
    }

    @Test
    public void executeItemQueryComponent() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        ItemConnection result = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);

        assertEquals(10, result.getEdges().size());
        assertEquals("MQ==", result.getEdges().get(0).getCursor());
        assertEquals(Component.class, result.getEdges().get(0).getNode().getClass());
        assertEquals("Core", result.getEdges().get(0).getNode().getTitle());
        assertEquals(ItemTypes.COMPONENT.getValue(), result.getEdges().get(0).getNode().getItemType());
    }

    @Test
    public void executeItemQueryKeyword() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.KEYWORD));
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        ItemConnection result = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);

        assertEquals(10, result.getEdges().size());
        assertEquals("MQ==", result.getEdges().get(0).getCursor());
        assertEquals(Keyword.class, result.getEdges().get(0).getNode().getClass());
        assertEquals("001 Top-level Keyword 1", result.getEdges().get(0).getNode().getTitle());
        assertEquals(ItemTypes.KEYWORD.getValue(), result.getEdges().get(0).getNode().getItemType());
    }

    @Test
    public void executeItemQueryPublication() throws Exception {

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.PUBLICATION));
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        ItemConnection result = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);

        assertEquals(7, result.getEdges().size());
        assertEquals("MQ==", result.getEdges().get(0).getCursor());
        assertEquals(Publication.class, result.getEdges().get(0).getNode().getClass());
        assertEquals("400 Example Site", result.getEdges().get(0).getNode().getTitle());
        assertEquals(ItemTypes.PUBLICATION.getValue(), result.getEdges().get(0).getNode().getItemType());
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
        TaxonomySitemapItem result = publicContentApi.getSitemap(ContentNamespace.Sites, 8, 2,
                new ContextData());

        assertEquals("t2680", result.getId());
        assertEquals(4, result.getItems().size());
        assertEquals("Used for Taxonomy-based Navigation purposes", result.getDescription());
    }

    @Test
    public void executeGetSitemapSubtree() {
        TaxonomySitemapItem[] result = publicContentApi.getSitemapSubtree(ContentNamespace.Sites, 8,
                "t2680-k10019", 2, true, new ContextData());

        assertEquals("t2680", result[0].getId());
        assertEquals(1, result[0].getItems().size());
        assertEquals("Used for Taxonomy-based Navigation purposes", result[0].getDescription());

    }

    @Test
    public void executeResolveBinaryLink() {
        String result = publicContentApi.resolveBinaryLink(ContentNamespace.Sites, 8, 756, "[#def#]", true);
        assertEquals("/media/balloons_tcm8-756.jpg", result);
    }

    @Test
    public void executeResolvePageLink() {
        String result = publicContentApi.resolvePageLink(ContentNamespace.Sites, 8, 4447, true);
        assertEquals("/system/include/content-tools.html", result);
    }

    @Test
    public void executeResolveComponentLink() {
        String result = publicContentApi.resolveComponentLink(ContentNamespace.Sites, 8, 3286, 640, 3292, true);
        assertEquals("/articles/all-articles.html", result);
    }

    @Test
    public void executeResolveDynamicComponentLink() {
        String result = publicContentApi.resolveDynamicComponentLink(ContentNamespace.Sites, 1082, 4569, 4565, 9195, true);
        assertEquals("/example-legacy/articles/news/news1.html", result);
    }

    @Test
    public void executegetPulbicationMapping() {
        PublicationMapping result = publicContentApi.getPublicationMapping(ContentNamespace.Sites, "http://localhost:8882/");

        assertEquals(PublicationMapping.class, result.getClass());
        assertEquals(5, result.getPublicationId());
        assertEquals("http", result.getProtocol());
        assertEquals("localhost", result.getDomain());
        assertEquals("8882", result.getPort());
        assertEquals("/", result.getPath());
        assertEquals(100, result.getPathScanDepth());
    }

    @Test
    public void executeGetPublication() {
        Publication result = publicContentApi.getPublication(ContentNamespace.Sites, 8, new ContextData(), "");

        assertEquals(Publication.class, result.getClass());
        assertEquals("dec06688-3c29-36e6-9f91-710c6109aab5", result.getId());
        assertEquals(1, result.getNamespaceId());
        assertEquals(8, result.getPublicationId());
        assertEquals("400 Example Site", result.getTitle());
        assertEquals("/", result.getPublicationUrl());
        assertEquals("\\", result.getPublicationPath());
        assertEquals("\\media", result.getMultimediaPath());
        assertEquals("/media/", result.getMultimediaUrl());
    }

    @Test
    public void executeGetPublications() {
        Pagination pagination = new Pagination();
        pagination.setFirst(1);
        PublicationConnection result = publicContentApi.getPublications(ContentNamespace.Sites, pagination, null, new ContextData(), "");

        assertEquals(PublicationConnection.class, result.getClass());
        assertEquals("MQ==", result.getEdges().get(0).getCursor());
        assertEquals("dec06688-3c29-36e6-9f91-710c6109aab5", result.getEdges().get(0).getNode().getId());
        assertEquals(8, result.getEdges().get(0).getNode().getPublicationId());
        assertEquals(1, result.getEdges().get(0).getNode().getNamespaceId());
        assertEquals("400 Example Site", result.getEdges().get(0).getNode().getTitle());
        assertEquals("/", result.getEdges().get(0).getNode().getPublicationUrl());
        assertEquals("\\", result.getEdges().get(0).getNode().getPublicationPath());
        assertEquals("\\media", result.getEdges().get(0).getNode().getMultimediaPath());
        assertEquals("/media/", result.getEdges().get(0).getNode().getMultimediaUrl());
    }
}