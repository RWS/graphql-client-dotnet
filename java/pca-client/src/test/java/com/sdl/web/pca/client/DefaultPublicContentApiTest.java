package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.sdl.web.pca.client.contentmodel.generated.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.generated.TaxonomySitemapItem;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.util.CmUri;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.runners.MockitoJUnitRunner;

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

        TaxonomySitemapItem result = publicContentApi.getSitemapSubtree(ContentNamespace.Sites, 8, "t2680-k10019",
                2, true, new ContextData());

        assertEquals("t2680", result.getId());
        assertEquals(1, result.getItems().size());
        assertEquals("Used for Taxonomy-based Navigation purposes", result.getDescription());
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
}