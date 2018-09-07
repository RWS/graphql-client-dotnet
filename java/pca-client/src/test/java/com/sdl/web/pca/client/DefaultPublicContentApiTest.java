package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.generated.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.generated.InputClaimValue;
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
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.runners.MockitoJUnitRunner;

import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import static com.sdl.web.pca.client.TestUtils.assertEqualsIgnoreSpaces;
import static com.sdl.web.pca.client.TestUtils.loadFromResource;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.mockito.Matchers.any;
import static org.mockito.Mockito.when;

@RunWith(MockitoJUnitRunner.class)
public class DefaultPublicContentApiTest {

    @Mock
    private GraphQLClient graphQlClient;

    @InjectMocks
    private DefaultPublicContentApi publicContentApi = new DefaultPublicContentApi(graphQlClient);

    @Test
    public void getPageModelDataByUrl() throws Exception {
        String expected = loadFromResource("getPageModelDataByUrlExpected");
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getPageModelDataByUrl"));

        JsonNode result = publicContentApi.getPageModelData(ContentNamespace.Sites, 1082,
                "/example-legacy/index.html", ContentType.MODEL, DataModelType.R2, PageInclusion.INCLUDE,
                false, new ContextData());

        assertEqualsIgnoreSpaces(expected, result.toString());
    }

    @Test
    public void getPageModelDataById() throws Exception {
        String expected = loadFromResource("getPageModelDataByIdExpected");
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getPageModelDataById"));

        JsonNode result = publicContentApi.getPageModelData(ContentNamespace.Sites, 1082, 640,
                ContentType.MODEL, DataModelType.DD4T, PageInclusion.INCLUDE, false, new ContextData());

        assertEqualsIgnoreSpaces(expected, result.toString());
    }

    @Test
    public void getEntityModelData() throws Exception {
        String expected = loadFromResource("getEntityModelDataExpected");
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getEntityModelData"));

        JsonNode result = publicContentApi.getEntityModelData(ContentNamespace.Sites, 8, 1458, 9195,
                ContentType.MODEL, DataModelType.R2, DcpType.DEFAULT, false, new ContextData());

        assertEqualsIgnoreSpaces(expected, result.toString());
    }

    @Test
    public void getSitemap() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getSitemap"));

        TaxonomySitemapItem result = publicContentApi.getSitemap(ContentNamespace.Sites, 8, 2,
                new ContextData());

        assertEquals("t2680", result.getId());
        assertEquals(4, result.getItems().size());
        assertEquals("Used for Taxonomy-based Navigation purposes", result.getDescription());
    }

    @Test
    public void getSitemapSubtree() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getSitemapSubtree"));

        TaxonomySitemapItem[] result = publicContentApi.getSitemapSubtree(ContentNamespace.Sites, 8, "t2680-k10019",
                2, true, new ContextData());

        assertEquals("t2680", result[0].getId());
        assertEquals(1, result[0].getItems().size());
        assertEquals("Used for Taxonomy-based Navigation purposes", result[0].getDescription());
    }

    @Test
    public void getBinaryComponentById() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getBinaryComponentById"));

        BinaryComponent result = publicContentApi.getBinaryComponent(ContentNamespace.Sites, 8, 756,
                null);

        assertEquals("b4e5c7c4-f04a-3d6d-898f-5886d0f648bd", result.getId());
        assertEquals("tcd:pub[8]/componentmeta[756]", result.getTitle());
        assertEquals("http://localhost:8081/udp/content/binary/1/8/756", result.getVariants().getEdges().get(0)
                .getNode().getDownloadUrl());
    }

    @Test
    public void getBinaryComponentByUri() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getBinaryComponentById"));

        BinaryComponent result = publicContentApi.getBinaryComponent(ContentNamespace.Sites, 8,
                "/media/balloons_tcm8-756.jpg", null);

        assertEquals("b4e5c7c4-f04a-3d6d-898f-5886d0f648bd", result.getId());
        assertEquals("tcd:pub[8]/componentmeta[756]", result.getTitle());
        assertEquals("http://localhost:8081/udp/content/binary/1/8/756", result.getVariants().getEdges().get(0)
                .getNode().getDownloadUrl());
    }

    @Test
    public void getBinaryComponentCmUri() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getBinaryComponentCmUri"));

        BinaryComponent result = publicContentApi.getBinaryComponent(new CmUri("tcm:8-756-16"), new ContextData());
        assertNotNull(result);

        assertEquals("b4e5c7c4-f04a-3d6d-898f-5886d0f648bd", result.getId());
        assertEquals("tcd:pub[8]/componentmeta[756]", result.getTitle());
        assertEquals("http://localhost:8081/udp/content/binary/1/8/756", result.getVariants().getEdges().get(0)
                .getNode().getDownloadUrl());
    }

    @Test
    public void executeItemQueryPage() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("executeItemQueryPage"));

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
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("executeItemQueryComponent"));

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(ContentNamespace.Sites.getNameSpaceValue()));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        ItemConnection result = publicContentApi.executeItemQuery(filter, null, pagination, null,
                null, false);
        assertNotNull(result);
        //TODO fix and add assertions

    }

    @Test
    public void executeItemQueryKeyword() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("executeItemQueryKeyword"));

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
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("executeItemQueryPublication"));

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
    public void resolvePageLink() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("resolvePageLink"));

        String result = publicContentApi.resolvePageLink(ContentNamespace.Sites, 8, 4447, true);
        assertEquals("/system/include/content-tools.html", result);
    }

    @Test
    public void resolveComponentLink() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("resolveComponentLink"));

        String result = publicContentApi.resolveComponentLink(ContentNamespace.Sites, 8, 3286, 640, 3292, true);
        assertEquals("/articles/all-articles.html", result);
    }

    @Test
    public void resolveBinaryLink() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("resolveBinaryLink"));

        String result = publicContentApi.resolveBinaryLink(ContentNamespace.Sites, 8, 756, "[#def#]", true);
        assertEquals("/media/balloons_tcm8-756.jpg", result);
    }

    @Test
    public void resolveDynamicComponentLink() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("resolveDynamicComponentLink"));

        String result = publicContentApi.resolveDynamicComponentLink(ContentNamespace.Sites, 1082, 4569, 4565, 9195, true);
        assertEquals("/example-legacy/articles/news/news1.html", result);
    }

    @Test
    public void getPublicationMapping() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class)))
                .thenReturn(loadFromResource("getPublicationMapping"));

        PublicationMapping result = publicContentApi.getPublicationMapping(ContentNamespace.Sites, "http://localhost:8882/");
        assertEquals(5, result.getPublicationId());
    }

    @Test
    public void getPublication() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class))).thenReturn(loadFromResource("getPublication"));

        Publication result = publicContentApi.getPublication(ContentNamespace.Sites, 8, new ContextData(), "");
        assertNotNull(result);

        assertEquals("dec06688-3c29-36e6-9f91-710c6109aab5", result.getId());
        assertEquals(8, result.getPublicationId());
        assertEquals("400 Example Site", result.getTitle());
        assertEquals("/", result.getPublicationUrl());
    }

    @Test
    public void getPublications() throws Exception {
        when(graphQlClient.execute(any(GraphQLRequest.class))).thenReturn(loadFromResource("getPublications"));

        Pagination pagination = new Pagination();
        pagination.setFirst(1);
        PublicationConnection result = publicContentApi.getPublications(ContentNamespace.Sites, pagination, null, new ContextData(), "");
        assertNotNull(result);

        assertEquals("dec06688-3c29-36e6-9f91-710c6109aab5", result.getEdges().get(0).getNode().getId());
        assertEquals(8, result.getEdges().get(0).getNode().getPublicationId());
        assertEquals("400 Example Site", result.getEdges().get(0).getNode().getTitle());
        assertEquals(1, result.getEdges().get(0).getNode().getNamespaceId());
        assertEquals("/", result.getEdges().get(0).getNode().getPublicationUrl());
    }

    @Test
    public void testMapToFragmentList() {
        InputItemFilter filter = new InputItemFilter();
        filter.setItemTypes(Arrays.asList(ItemType.CATEGORY, ItemType.COMPONENT_PRESENTATION, ItemType.STRUCTURE_GROUP,
                ItemType.PAGE));
        List<String> expected = Arrays.asList("CategoryFields", "ComponentPresentationFields", "StructureGroupFields", "PageFields");

        assertEquals(expected, publicContentApi.mapToFragmentList(filter));
    }
}