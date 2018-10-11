package com.sdl.web.pca.client;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.module.SimpleModule;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.ContentIncludeMode;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.generated.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.generated.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputPublicationFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputSortParam;
import com.sdl.web.pca.client.contentmodel.generated.Item;
import com.sdl.web.pca.client.contentmodel.generated.ItemConnection;
import com.sdl.web.pca.client.contentmodel.generated.Page;
import com.sdl.web.pca.client.contentmodel.generated.Publication;
import com.sdl.web.pca.client.contentmodel.generated.PublicationConnection;
import com.sdl.web.pca.client.contentmodel.generated.PublicationMapping;
import com.sdl.web.pca.client.contentmodel.generated.SitemapItem;
import com.sdl.web.pca.client.contentmodel.generated.TaxonomySitemapItem;
import com.sdl.web.pca.client.exception.GraphQLClientException;
import com.sdl.web.pca.client.exception.PublicContentApiException;
import com.sdl.web.pca.client.jsonmapper.ItemDeserializer;
import com.sdl.web.pca.client.jsonmapper.SitemapDeserializer;
import com.sdl.web.pca.client.query.PCARequestBuilder;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.util.CmUri;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.concurrent.TimeUnit;
import java.util.stream.Collectors;

import static com.sdl.web.pca.client.modelserviceplugin.ClaimHelper.createClaim;

public class DefaultPublicContentApi implements PublicContentApi {
    private static final ObjectMapper MAPPER = new ObjectMapper();

    private GraphQLClient client;
    private int requestTimeout;
    private ContextData defaultContextData;

    public DefaultPublicContentApi(GraphQLClient graphQLClient) {
        this(graphQLClient, 0);
    }

    public DefaultPublicContentApi(GraphQLClient graphQLClient, int requestTimeout) {
        this.client = graphQLClient;
        this.requestTimeout = (int) TimeUnit.MILLISECONDS.toMillis(requestTimeout);
        this.defaultContextData = new ContextData();

        SimpleModule module = new SimpleModule();
        module.addDeserializer(SitemapItem.class, new SitemapDeserializer(SitemapItem.class, MAPPER));
        module.addDeserializer(Item.class, new ItemDeserializer(Item.class, MAPPER));
        MAPPER.registerModule(module);
    }


    @Override
    public Page getPage(ContentNamespace ns, int publicationId, int pageId, String customMetaFilter, ContentIncludeMode contentIncludeMode, ContextData contextData) {
        return null;
    }

    @Override
    public Page getPage(ContentNamespace ns, int publicationId, String url, String customMetaFilter, ContentIncludeMode contentIncludeMode, ContextData contextData) {
        return null;
    }

    @Override
    public Page getPage(ContentNamespace ns, int publicationId, CmUri cmUri, String customMetaFilter, ContentIncludeMode contentIncludeMode, ContextData contextData) {
        return null;
    }

    @Override
    public BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
                                              ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("BinaryComponentById", true)
                .withVariantArgs(null)
                .withCustomMetaFilter(null)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("binaryId", binaryId)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, BinaryComponent.class, "/data/binaryComponent");
    }

    @Override
    public BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url,
                                              ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("BinaryComponentByUrl", true)
                .withVariantArgs(url)
                .withCustomMetaFilter(null)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("url", url)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, BinaryComponent.class, "/data/binaryComponent");
    }

    @Override
    public BinaryComponent getBinaryComponent(CmUri cmUri, ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("BinaryComponentByCmUri", true)
                .withVariantArgs(null)
                .withCustomMetaFilter(null)
                .withVariable("namespaceId", cmUri.getNamespaceId())
                .withVariable("publicationId", cmUri.getPublicationId())
                .withVariable("cmUri", cmUri.toString())
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, BinaryComponent.class, "/data/binaryComponent");
    }

    @Override
    public ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, Pagination pagination,
                                           ContextData contextData, String customMetaFilter,
                                           boolean renderContent) throws PublicContentApiException {

        // We only include the fragments that will be required based on the item types in the
        // input item filter
        List<String> fragments = new ArrayList<>();
        if (filter != null && filter.getItemTypes() != null) {
            fragments = mapToFragmentList(filter);
        }

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQueryAndFragmentList("ItemQuery", fragments)
                .withCustomMetaFilter(customMetaFilter)
                .withRenderContentArgs(renderContent)
                .withVariable("first", pagination.getFirst())
                .withVariable("after", pagination.getAfter())
                .withVariable("filter", filter)
                .withVariable("sort", sort)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, ItemConnection.class, "/data/items");
    }

    List<String> mapToFragmentList(InputItemFilter filter) {
        return filter.getItemTypes().stream().map(type -> Arrays.stream(type.toString().split("_"))
                .map(s -> s.substring(0, 1).toUpperCase() + s.substring(1).toLowerCase())
                .reduce("", String::concat)
                + "Fields"
        ).collect(Collectors.toList());
    }

    @Override
    public Publication getPublication(ContentNamespace ns, int publicationId, ContextData contextData,
                                      String customMetaFilter) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("Publication", true)
                .withCustomMetaFilter(customMetaFilter)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, Publication.class, "/data/publication");
    }

    @Override
    public PublicationConnection getPublications(ContentNamespace ns, Pagination pagination,
                                                 InputPublicationFilter filter, ContextData contextData,
                                                 String customMetaFilter) {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("Publications", true)
                .withCustomMetaFilter(customMetaFilter)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("first", pagination.getFirst())
                .withVariable("after", pagination.getAfter())
                .withVariable("filter", filter)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, PublicationConnection.class, "/data/publications");
    }

    @Override
    public String resolvePageLink(ContentNamespace ns, int publicationId, int pageId,
                                  boolean renderRelativeLink) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("ResolvePageLink")
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("pageId", pageId)
                .withVariable("renderRelativeLink", renderRelativeLink)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/pageLink/url").asText();
    }

    @Override
    public String resolveComponentLink(ContentNamespace ns, int publicationId, int componentId, Integer sourcePageId,
                                       Integer excludeComponentTemplateId,
                                       boolean renderRelativeLink) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("ResolveComponentLink")
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("targetComponentId", componentId)
                .withVariable("sourcePageId", sourcePageId)
                .withVariable("excludeComponentTemplateId", excludeComponentTemplateId)
                .withVariable("renderRelativeLink", renderRelativeLink)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/componentLink/url").asText();
    }

    @Override
    public String resolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
                                    String variantId, boolean renderRelativeLink) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("ResolveBinaryLink")
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("binaryId", binaryId)
                .withVariable("variantId", variantId)
                .withVariable("renderRelativeLink", renderRelativeLink)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/binaryLink/url").asText();
    }

    @Override
    public String resolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
                                              int templateId,
                                              boolean renderRelativeLink) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("ResolveDynamicComponentLink")
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("targetPageId", pageId)
                .withVariable("targetComponentId", componentId)
                .withVariable("targetTemplateId", templateId)
                .withVariable("renderRelativeLink", renderRelativeLink)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/dynamicComponentLink/url").asText();
    }

    @Override
    public PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("PublicationMapping", true)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("siteUrl", url)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, PublicationMapping.class, "/data/publicationMapping");
    }

    @Override
    public JsonNode getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                                     DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                     ContextData contextData) throws PublicContentApiException {
        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("PageModelByUrl")
                .withRenderContentArgs(renderContent)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("url", url)
                .withContextData(defaultContextData, contextData)
                .withClaim(createClaim(contentType))
                .withClaim(createClaim(modelType))
                .withClaim(createClaim(pageInclusion))
                .withOperation("page")
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/page/rawContent/data");
    }

    @Override
    public JsonNode getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                                     DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                     ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("PageModelById")
                .withRenderContentArgs(renderContent)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("pageId", pageId)
                .withContextData(defaultContextData, contextData)
                .withClaim(createClaim(contentType))
                .withClaim(createClaim(modelType))
                .withClaim(createClaim(pageInclusion))
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/page/rawContent/data");
    }

    @Override
    public JsonNode getEntityModelData(ContentNamespace ns, int publicationId, int componentId, int templateId,
                                       ContentType contentType, DataModelType modelType, DcpType dcpType,
                                       boolean renderContent, ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("EntityModelById")
                .withRenderContentArgs(renderContent)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("componentId", componentId)
                .withVariable("templateId", templateId)
                .withContextData(defaultContextData, contextData)
                .withClaim(createClaim(contentType))
                .withClaim(createClaim(modelType))
                .withClaim(createClaim(dcpType))
                .withTimeout(requestTimeout)
                .buildRequest();

        return getJsonResult(graphQLRequest, "/data/componentPresentation/rawContent/data");
    }

    @Override
    public TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                          ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("Sitemap")
                .withFragment("TaxonomyItemFields")
                .withFragment("TaxonomyPageFields")
                .withRecurseFragmentApplied("RecurseItems", descendantLevels)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, TaxonomySitemapItem.class, "/data/sitemap");
    }

    @Override
    public TaxonomySitemapItem[] getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                                   int descendantLevels, boolean includeAncestors,
                                                   ContextData contextData) throws PublicContentApiException {

        GraphQLRequest graphQLRequest = new PCARequestBuilder()
                .withQuery("SitemapSubtree")
                .withFragment("TaxonomyItemFields")
                .withFragment("TaxonomyPageFields")
                .withRecurseFragmentApplied("RecurseItems", descendantLevels)
                .withVariable("namespaceId", ns.getNameSpaceValue())
                .withVariable("publicationId", publicationId)
                .withVariable("taxonomyNodeId", taxonomyNodeId)
                .withVariable("includeAncestors", includeAncestors)
                .withContextData(defaultContextData, contextData)
                .withTimeout(requestTimeout)
                .buildRequest();

        return getResultForRequest(graphQLRequest, TaxonomySitemapItem[].class, "/data/sitemapSubtree");
    }


    private <T> T getResultForRequest(GraphQLRequest request, Class<T> clazz, String path) throws PublicContentApiException {
        JsonNode result = getJsonResult(request, path);
        try {
            return MAPPER.treeToValue(result, clazz);
        } catch (JsonProcessingException e) {
            throw new PublicContentApiException("Unable map result to " + clazz.getName() + ": " + result.toString(), e);
        }
    }

    private JsonNode getJsonResult(GraphQLRequest request, String path) throws PublicContentApiException {
        try {
            String resultString = client.execute(request);
            JsonNode resultJson = MAPPER.readTree(resultString);
            return resultJson.at(path);
        } catch (GraphQLClientException e) {
            throw new PublicContentApiException("Unable to execute query: " + request, e);
        } catch (IOException e) {
            throw new PublicContentApiException("Unable to deserialize result for query " + request, e);
        }
    }
}
