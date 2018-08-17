package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sdl.web.pca.client.contentmodel.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.ContentQuery;
import com.sdl.web.pca.client.contentmodel.ContentType;
import com.sdl.web.pca.client.contentmodel.IContextData;
import com.sdl.web.pca.client.contentmodel.IPagination;
import com.sdl.web.pca.client.contentmodel.InputClaimValue;
import com.sdl.web.pca.client.contentmodel.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.ItemType;
import com.sdl.web.pca.client.contentmodel.Page;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.exception.GraphQLClientException;
import com.sdl.web.pca.client.exception.PublicContentApiException;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.CUSTOM_META_FIELDS_FRAGMENT;
import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.ENTITY_MODEL_BY_ID;
import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.ITEM_FIELDS_FRAGMENT;
import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.ITEM_QUERY;
import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.PAGE_MODEL_BY_ID;
import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.SITEMAP;
import static com.sdl.web.pca.client.contentmodel.query.ContentQueries.SITEMAP_SUBTREE;
import static java.nio.charset.StandardCharsets.UTF_8;

public class PublicContentApi implements IPublicContentApi {
    private static final ObjectMapper MAPPER = new ObjectMapper();

    private GraphQLClient client;
    private Map<String, String> queries = new ConcurrentHashMap<>();

    public PublicContentApi(GraphQLClient graphQLClient) {
        client = graphQLClient;
    }

    @Override
    public <T> T executeItemQuery(InputItemFilter filter, IPagination pagination,
                                  Class<T> clazz) throws PublicContentApiException {
        String customMetaFilter = "";
        String query = getQueryFor(ITEM_QUERY);
        query += getQueryFor(ITEM_FIELDS_FRAGMENT);
        query += getQueryFor(CUSTOM_META_FIELDS_FRAGMENT);

        // We only include the fragments that will be required based on the item types in the
        // input item filter
        if (filter.getItemTypes() != null) {
            String fragmentList = "";
            for (ItemType itemType : filter.getItemTypes()) {
                String fragment = itemType.name().substring(0, 1).toUpperCase()
                        + itemType.name().substring(1).toLowerCase() + "Fields";
                query += getQueryFor(fragment + "Fragment");
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

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        return getResultForRequest(graphQLRequest, clazz);
    }


    @Override
    public <T> T getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                                  DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                                  IContextData contextData, Class<T> clazz) throws PublicContentApiException {

        String query = getQueryFor(PAGE_MODEL_BY_ID);
        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("pageId", pageId);
        variables.put("contextData", contextData);

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        return getResultForRequest(graphQLRequest, clazz);
    }

    @Override
    public <T> T executeSiteMap(Page page, Class<T> clazz) throws PublicContentApiException {
        String query = getQueryFor(SITEMAP);
        query += getQueryFor("RecurseItems");
        query += getQueryFor("TaxonomyItemFields");
        query += getQueryFor("TaxonomyPageFields");

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", page.getNamespaceId());
        variables.put("publicationId", page.getPublicationId());

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        return getResultForRequest(graphQLRequest, clazz);
    }

    @Override
    public <T> T getSitemap(ContentNamespace ns, int publicationId, Class<T> clazz) throws PublicContentApiException {
        String query = getQueryFor(SITEMAP);
        query += getQueryFor("RecurseItems");
        query += getQueryFor("TaxonomyItemFields");
        query += getQueryFor("TaxonomyPageFields");

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        return getResultForRequest(graphQLRequest, clazz);
    }

    @Override
    public <T> T getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                   boolean includeAncestors, Class<T> clazz) throws PublicContentApiException {
        String query = getQueryFor(SITEMAP_SUBTREE);
        query += getQueryFor("TaxonomyItemFields");
        query += getQueryFor("RecurseItems");
        query += getQueryFor("TaxonomyPageFields");

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("taxonomyNodeId", taxonomyNodeId);
        variables.put("includeAncestors", includeAncestors);

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        return getResultForRequest(graphQLRequest, clazz);
    }


    @Override
    public <T> T getEntityModelData(ContentNamespace ns, int publicationId, int componentId, int templateId, Class<T> clazz) throws PublicContentApiException {
        String query = getQueryFor(ENTITY_MODEL_BY_ID);

        HashMap<String, Object> variables = new HashMap<>();

        variables.put("namespaceId", ns.getNameSpaceValue());
        variables.put("publicationId", publicationId);
        variables.put("componentId", componentId);
        variables.put("templateId", templateId);

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        return getResultForRequest(graphQLRequest, clazz);
    }

    private String getQueryFor(String queryName) throws PublicContentApiException {
        return queries.computeIfAbsent(queryName, s -> loadQueryFromResourcefile(s));
    }

    private String loadQueryFromResourcefile(String queryName) throws PublicContentApiException {
        String path = "queries/" + queryName + ".graphql";
        try {
            return IOUtils.toString(ContentQuery.class.getClassLoader().getResourceAsStream(path), UTF_8);
        } catch (IOException e) {
            throw new PublicContentApiException("Unable to read resource " + path, e);
        }
    }

    private <T> T getResultForRequest(GraphQLRequest request, Class<T> clazz) throws PublicContentApiException {
        try {
            String contentQuery = client.execute(request);
            MAPPER.enable(DeserializationFeature.UNWRAP_ROOT_VALUE);
            return MAPPER.readValue(contentQuery, clazz);
        } catch (GraphQLClientException e) {
            throw new PublicContentApiException("Unable to execute query: " + request, e);
        } catch (IOException e) {
            throw new PublicContentApiException("Unable to deserialize result for query " + request, e);
        }
    }
}
