package com.sdl.web.pca.client;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.ClaimValue;
import com.sdl.web.pca.client.contentmodel.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.ContentType;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.IContextData;
import com.sdl.web.pca.client.contentmodel.IPagination;
import com.sdl.web.pca.client.contentmodel.InputClaimValue;
import com.sdl.web.pca.client.contentmodel.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.InputPublicationFilter;
import com.sdl.web.pca.client.contentmodel.InputSortParam;
import com.sdl.web.pca.client.contentmodel.ItemConnection;
import com.sdl.web.pca.client.contentmodel.ItemType;
import com.sdl.web.pca.client.contentmodel.Publication;
import com.sdl.web.pca.client.contentmodel.PublicationConnection;
import com.sdl.web.pca.client.contentmodel.PublicationMapping;
import com.sdl.web.pca.client.contentmodel.TaxonomySitemapItem;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.exception.GraphQLClientException;
import com.sdl.web.pca.client.exception.PublicContentApiException;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.util.*;

import static com.sdl.web.pca.client.modelserviceplugin.ClaimHelper.createClaim;
import static java.nio.charset.StandardCharsets.UTF_8;

public class DefaultPublicContentApi implements PublicContentApi {
    private static final ObjectMapper MAPPER = new ObjectMapper();

    private GraphQLClient client;
    private Map<String, String> queries = new HashMap<>();
    private Map<String, String> fragments = new HashMap<>();
    private int requestTimeout;
    private ContextData defaultContextData;

    public DefaultPublicContentApi(GraphQLClient graphQLClient) {
        this(graphQLClient, 0);
    }

    public DefaultPublicContentApi(GraphQLClient graphQLClient, int requestTimeout) {
        this.client = graphQLClient;
        this.requestTimeout = requestTimeout;
        this.defaultContextData = new ContextData();
        this.defaultContextData.setClaimValues(Collections.EMPTY_LIST);
    }

    @Override
    public JsonNode getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                                     DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                     ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        List<ClaimValue> claims = Arrays.asList(
                createClaim(contentType),
                createClaim(modelType),
                createClaim(pageInclusion)
        );
        mergedData.getClaimValues().addAll(claims);

        String query = getQueryFor("PageModelByUrl");
        query = QueryUtils.injectRenderContentArgs(query, renderContent);
        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("url", url);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, "page", requestTimeout);

        return getJsonResult(graphQLRequest, "/data/page/rawContent/data");
    }

    @Override
    public JsonNode getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                                     DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                     ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        List<ClaimValue> claims = Arrays.asList(
                createClaim(contentType),
                createClaim(modelType),
                createClaim(pageInclusion)
        );
        mergedData.getClaimValues().addAll(claims);
        String query = getQueryFor("PageModelById");
        query = QueryUtils.injectRenderContentArgs(query, renderContent);
        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("pageId", pageId);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);

        return getJsonResult(graphQLRequest, "/data/page/rawContent/data");
    }

    @Override
    public JsonNode getEntityModelData(ContentNamespace ns, int publicationId, int componentId, int templateId,
                                       ContentType contentType, DataModelType modelType, DcpType dcpType,
                                       boolean renderContent, ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        List<ClaimValue> claims = Arrays.asList(
                createClaim(contentType),
                createClaim(modelType),
                createClaim(dcpType)
        );
        mergedData.getClaimValues().addAll(claims);
        String query = getQueryFor("EntityModelById");
        query = QueryUtils.injectRenderContentArgs(query, renderContent);
        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("componentId", componentId);
        variables.put("templateId", templateId);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getJsonResult(graphQLRequest, "/data/componentPresentation/rawContent/data");
    }

    @Override
    public TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                          ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        String query = getQueryFor("Sitemap");
        query += getFragmentFor("TaxonomyItemFields");
        query += getFragmentFor("TaxonomyPageFields");
        String recurseItems = getFragmentFor("RecurseItems");
        query = QueryUtils.expandRecursively(query, recurseItems, descendantLevels);

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, TaxonomySitemapItem.class, "/data/sitemap");
    }

    @Override
    public TaxonomySitemapItem getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                                 int descendantLevels, boolean includeAncestors,
                                                 ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        String query = getQueryFor("SitemapSubtree");
        query += getFragmentFor("TaxonomyItemFields");
        query += getFragmentFor("TaxonomyPageFields");
        String recurseItems = getFragmentFor("RecurseItems");
        query = QueryUtils.expandRecursively(query, recurseItems, descendantLevels);

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("taxonomyNodeId", taxonomyNodeId);
        variables.put("includeAncestors", includeAncestors);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, TaxonomySitemapItem.class, "/data/sitemapSubtree");
    }

    @Override
    public BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
                                              ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        String query = getQueryFor("BinaryComponentById");
        query += getFragmentFor("ItemFields");
        query += getFragmentFor("BinaryComponentFields");
        query += getFragmentFor("CustomMetaFields");
        query = QueryUtils.injectVariantsArgs(query, null);
        query = QueryUtils.injectCustomMetaFilter(query, null);

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("binaryId", binaryId);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, BinaryComponent.class, "/data/binaryComponent");
    }

    @Override
    public BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url,
                                              ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        String query = getQueryFor("BinaryComponentByUrl");
        query += getFragmentFor("ItemFields");
        query += getFragmentFor("BinaryComponentFields");
        query += getFragmentFor("CustomMetaFields");
        query = QueryUtils.injectVariantsArgs(query, url);
        query = QueryUtils.injectCustomMetaFilter(query, null);

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("url", url);
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, BinaryComponent.class, "/data/binaryComponent");
    }

    @Override
    public BinaryComponent getBinaryComponent(CmUri cmUri, ContextData contextData) throws PublicContentApiException {
        ContextData mergedData = mergeContextData(defaultContextData, contextData);
        String query = getQueryFor("BinaryComponentByCmUri");
        query += getFragmentFor("ItemFields");
        query += getFragmentFor("BinaryComponentFields");
        query += getFragmentFor("CustomMetaFields");
        query = QueryUtils.injectVariantsArgs(query, null);
        query = QueryUtils.injectCustomMetaFilter(query, null);

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", cmUri.getNamespaceId());
        variables.put("publicationId", cmUri.getPublicationId());
        variables.put("cmUri", cmUri.toString());
        variables.put("contextData", mergedData.getClaimValues());

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, BinaryComponent.class, "/data/binaryComponent");
    }

    @Override
    public ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
                                           ContextData contextData, String customMetaFilter,
                                           boolean renderContent) throws PublicContentApiException {
        //TODO fix: sort, contextData, customMetaFilter is not used in current implementation
        customMetaFilter = "";
        String query = getQueryFor("ItemQuery");
        query += getFragmentFor("ItemFields");
        query += getFragmentFor("CustomMetaFields");

        // We only include the fragments that will be required based on the item types in the
        // input item filter
        if (filter.getItemTypes() != null) {
            String fragmentList = "";
            for (ItemType itemType : filter.getItemTypes()) {
                String fragment = itemType.name().substring(0, 1).toUpperCase()
                        + itemType.name().substring(1).toLowerCase() + "Fields";
                query += getFragmentFor(fragment);
                fragmentList += "..." + fragment + "\n";
            }
            // Just a quick and easy way to replace markers in our queries with vars here.
            query = query.replace("@fragmentList", fragmentList);
            query = query.replace("@customMetaFilter", "\"" + customMetaFilter + "\"");
        }

        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("first", pagination.getFirst());
        variables.put("after", pagination.getAfter());
        variables.put("filter", filter);
        variables.put("contextData", inputClaimValues);

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, ItemConnection.class);
    }

    @Override
    public Publication getPublication(ContentNamespace ns, int publicationId, ContextData contextData, String customMetaFilter) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public PublicationConnection getPublications(ContentNamespace ns, IPagination pagination, InputPublicationFilter filter, IContextData contextData, String customMetaFilter) {
        //TODO implement
        return null;
    }

    @Override
    public String ResolvePageLink(ContentNamespace ns, int publicationId, int pageId) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public String ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, Integer sourcePageId,
                                       Integer excludeComponentTemplateId) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public String ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
                                    String variantId) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public String ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
                                              int templateId) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    private String getQueryFor(String queryName) throws PublicContentApiException {
        return queries.computeIfAbsent(queryName,
                s -> loadQueryFromResourcefile("queries/" + s));
    }

    private String getFragmentFor(String fragmentName) throws PublicContentApiException {
        return fragments.computeIfAbsent(fragmentName,
                s -> loadQueryFromResourcefile("queries/fragments/" + s));
    }

    private String loadQueryFromResourcefile(String fileName) throws PublicContentApiException {
        String path = fileName + ".graphql";
        try {
            return IOUtils.toString(this.getClass().getClassLoader().getResourceAsStream(path), UTF_8);
        } catch (IOException e) {
            throw new PublicContentApiException("Unable to read resource " + path, e);
        }
    }

    private <T> T getResultForRequest(GraphQLRequest request, Class<T> clazz) throws PublicContentApiException {
        try {
            String contentQuery = client.execute(request);
            return MAPPER.readValue(contentQuery, clazz);
        } catch (GraphQLClientException e) {
            throw new PublicContentApiException("Unable to execute query: " + request, e);
        } catch (IOException e) {
            throw new PublicContentApiException("Unable to deserialize result for query " + request, e);
        }
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

    private ContextData mergeContextData(ContextData data1, ContextData data2) {
        List<ClaimValue> merged = new ArrayList<>();
        if (data1 != null && data1.getClaimValues() != null) {
            merged.addAll(data1.getClaimValues());
        }
        if (data2 != null && data2.getClaimValues() != null) {
            merged.addAll(data2.getClaimValues());
        }
        ContextData result = new ContextData();
        result.setClaimValues(merged);
        return result;
    }

    public void setGraphQLClient(GraphQLClient client) {
        this.client = client;
    }
}
