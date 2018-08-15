package com.sdl.web.pca.client;


import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.ObjectReader;
import com.google.gson.JsonObject;
import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.exceptions.GraphQLClientException;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.util.HashMap;

public class PublicContentApi implements IPublicContentApi {

    private GraphQLClient _client;

    public PublicContentApi(GraphQLClient graphQLClient) {
        _client = graphQLClient;
    }

    private String loadQueryFromResourcefile(String filename) throws IOException {
        String query = IOUtils.toString(ContentQuery.class.getClassLoader().getResourceAsStream("queries/" + filename + ".graphql"), "UTF-8");
        return query;
    }

    public <T> T executeItemQuery(InputItemFilter filter, IPagination pagination, Class<T> clazz) throws GraphQLClientException, IOException {

        String customMetaFilter = "";
        String query = loadQueryFromResourcefile("ItemQuery");
        query += loadQueryFromResourcefile("ItemFieldsFragment");
        query += loadQueryFromResourcefile("CustomMetaFieldsFragment");

        // We only include the fragments that will be required based on the item types in the
        // input item filter
        if (filter.getItemTypes() != null) {
            String fragmentList = "";
            for (ItemType itemType : filter.getItemTypes()) {
                String fragment = itemType.name().substring(0, 1).toUpperCase() + itemType.name().substring(1).toLowerCase() + "Fields";
                query += loadQueryFromResourcefile(fragment + "Fragment");
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

        GraphQLResponse contentQuery = _client.execute(graphQLRequest);

        ObjectMapper objectMapper = new ObjectMapper();

        return objectMapper.readValue(contentQuery.getData().toString(), clazz);
    }


    public <T> T getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType, DataModelType modelType, PageInclusion pageInclusion, boolean renderContent, IContextData contextData, Class<T> clazz) {

        try {
            updateContextData(contextData, contentType, modelType, pageInclusion);
            String query = "";
            try {
                query = loadQueryFromResourcefile("PageModelById");
            } catch (Exception e) {
                String s = e.getMessage();
            }
            HashMap<String, Object> variables = new HashMap<>();
            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);
            variables.put("pageId", pageId);
            variables.put("contextData", contextData);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            GraphQLResponse contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            return objectMapper.readValue(contentQuery.getData().toString(), clazz);

        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }

    protected void updateContextData(IContextData contextData, ContentType contentType, DataModelType dataModelType, PageInclusion pageInclusion) {
        if (contextData == null) {
            contextData = new ContextData();
        }
    }


    public <T> T executeSiteMap(Page page, Class<T> model) throws IOException, GraphQLClientException {
        String query = loadQueryFromResourcefile("Sitemap");
        query += loadQueryFromResourcefile("RecurseItems");
        query += loadQueryFromResourcefile("TaxonomyItemFields");
        query += loadQueryFromResourcefile("TaxonomyPageFields");

        HashMap<String, Object> variables = new HashMap<>();
        variables.put("namespaceId", page.getNamespaceId());
        variables.put("publicationId", page.getPublicationId());

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        GraphQLResponse contentQuery = _client.execute(graphQLRequest);

        ObjectMapper objectMapper = new ObjectMapper();
        ObjectReader objectReader = objectMapper.reader(model).withRootName("data");

        T sitemap = objectReader.readValue(contentQuery.getData().toString());

        return sitemap;
    }

    public <T> T getSitemap(ContentNamespace ns, int publicationId, Class<T> clazz) {

        try {
            String query = loadQueryFromResourcefile("Sitemap");
            query += loadQueryFromResourcefile("RecurseItems");
            query += loadQueryFromResourcefile("TaxonomyItemFields");
            query += loadQueryFromResourcefile("TaxonomyPageFields");

            HashMap<String, Object> variables = new HashMap<>();
            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            GraphQLResponse contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            return objectMapper.readValue(contentQuery.getData().toString(), clazz);

        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }

    public <T> T getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId, boolean includeAncestors, Class<T> clazz) {
        try {
            String query = loadQueryFromResourcefile("SitemapSubtree");
            query += loadQueryFromResourcefile("TaxonomyItemFields");
            query += loadQueryFromResourcefile("RecurseItems");
            query += loadQueryFromResourcefile("TaxonomyPageFields");

            HashMap<String, Object> variables = new HashMap<>();
            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);
            variables.put("taxonomyNodeId", taxonomyNodeId);
            variables.put("includeAncestors", includeAncestors);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            GraphQLResponse contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            return objectMapper.readValue(contentQuery.getData().toString(), clazz);

        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }

    public <T> T getEntityModelData(ContentNamespace ns, int publicationId, int entityId, Class<T> clazz) {
        try {
            String query = loadQueryFromResourcefile("EntityModelById");

            HashMap<String, Object> variables = new HashMap<>();

            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);
            variables.put("entityId", entityId);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            GraphQLResponse contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            return objectMapper.readValue(contentQuery.getData().toString(), clazz);
        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }
}
