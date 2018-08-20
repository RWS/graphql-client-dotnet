package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.ContentQuery;
import com.sdl.web.pca.client.contentmodel.ContentType;
import com.sdl.web.pca.client.contentmodel.IContextData;
import com.sdl.web.pca.client.contentmodel.IPagination;
import com.sdl.web.pca.client.contentmodel.InputClaimValue;
import com.sdl.web.pca.client.contentmodel.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.InputSortParam;
import com.sdl.web.pca.client.contentmodel.ItemConnection;
import com.sdl.web.pca.client.contentmodel.ItemType;
import com.sdl.web.pca.client.contentmodel.Publication;
import com.sdl.web.pca.client.contentmodel.PublicationMapping;
import com.sdl.web.pca.client.contentmodel.TaxonomySitemapItem;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.exception.GraphQLClientException;
import com.sdl.web.pca.client.exception.PublicContentApiException;
import com.sdl.web.pca.client.modelserviceplugin.ModelServicePluginApi;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import static java.nio.charset.StandardCharsets.UTF_8;

public class DefaultPublicContentApi implements PublicContentApi, ModelServicePluginApi {
    private static final ObjectMapper MAPPER = new ObjectMapper().enable(DeserializationFeature.UNWRAP_ROOT_VALUE);

    private GraphQLClient client;
    private Map<String, String> queries = new HashMap<>();
    private Map<String, String> fragments = new HashMap<>();
    private int requestTimeout;

    public DefaultPublicContentApi(GraphQLClient graphQLClient) {
        client = graphQLClient;
    }

    public DefaultPublicContentApi(GraphQLClient graphQLClient, int requestTimeout) {
        this(graphQLClient);
        this.requestTimeout = requestTimeout;
    }


    @Override
    public <T> T getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                                  DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                  IContextData contextData, Class<T> clazz) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public <T> T getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                                  DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                  IContextData contextData, Class<T> clazz) throws PublicContentApiException {
        //TODO fix: pageInclusion, modelType, renderContent is not used
        String query = getQueryFor("PageModelById");
        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("pageId", pageId);
        variables.put("contextData", contextData);

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, clazz);
    }

    @Override
    public <T> T getEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
                                    DataModelType modelType, DcpType dcpType, boolean renderContent,
                                    IContextData contextData, Class<T> clazz) throws PublicContentApiException {
        //TODO fix: contentType, modoelType, dcpType, renderContent is not used in current implementation
        String query = getQueryFor("EntityModelById");

        HashMap<String, Object> variables = new HashMap<>();

        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("entityId", entityId);

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, clazz);    }

     @Override
    public TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                          IContextData contextData) throws PublicContentApiException {
        //TODO fix: descendsantLevels, contextData is not used in current implementation
         String query = getQueryFor("Sitemap");
         query += getFragmentFor("RecurseItems");
         query += getFragmentFor("TaxonomyItemFields");
         query += getFragmentFor("TaxonomyPageFields");

         HashMap<String, Object> variables = new HashMap<>();
         variables.put("namespaceId", ns.getNameSpaceValue());
         variables.put("publicationId", publicationId);

         GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
         return getResultForRequest(graphQLRequest, TaxonomySitemapItem.class);
    }

    @Override
    public TaxonomySitemapItem getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                                 int descendantLevels, boolean includeAncestors,
                                                 IContextData contextData) throws PublicContentApiException {
        //TODO fix: descendantLevels, includeAncestors, contextData is not used in current implementatioon
        String query = getQueryFor("SitemapSubtree");
        query += getFragmentFor("TaxonomyItemFields");
        query += getFragmentFor("RecurseItems");
        query += getFragmentFor("TaxonomyPageFields");

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("taxonomyNodeId", taxonomyNodeId);
        variables.put("includeAncestors", includeAncestors);

        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
        return getResultForRequest(graphQLRequest, TaxonomySitemapItem.class);
    }

    @Override
    public BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
                                              IContextData contextData) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url,
                                              IContextData contextData) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public BinaryComponent getBinaryComponent(CmUri cmUri, IContextData contextData) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
                                           IContextData contextData, String customMetaFilter,
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
    public Publication getPublication(ContentNamespace ns, int publicationId, IContextData contextData, String customMetaFilter) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public String resolveLink(CmUri cmUri, boolean resolveToBinary) throws PublicContentApiException {
        //TODO implement
        return null;
    }

    @Override
    public PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws PublicContentApiException {
        //TODO implement
        return null;
    }

//    @Override
//    public <T> T executeSiteMap(Page page, Class<T> clazz) throws PublicContentApiException {
//        String query = getQueryFor(SITEMAP);
//        query += getQueryFor("RecurseItems");
//        query += getQueryFor("TaxonomyItemFields");
//        query += getQueryFor("TaxonomyPageFields");
//
//        HashMap<String, Object> variables = new HashMap<>();
//        variables.put("namespaceId", page.getNamespaceId());
//        variables.put("publicationId", page.getPublicationId());
//
//        GraphQLRequest graphQLRequest = new GraphQLRequest(query, variables, requestTimeout);
//        return getResultForRequest(graphQLRequest, clazz);
//    }




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

    public GraphQLClient getGraphQLClient() {
        return client;
    }

    public void setGraphQLClient(GraphQLClient client) {
        this.client = client;
    }
}
