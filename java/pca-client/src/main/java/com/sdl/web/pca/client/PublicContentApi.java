package com.sdl.web.pca.client;


import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.ObjectReader;
import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.request.GraphQLRequest;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.util.HashMap;

public class PublicContentApi implements IPublicContentApi {

    public GraphQLClient _client;
    private ContentComponent  contentComponent;

    public PublicContentApi(GraphQLClient graphQLClient) {
        _client = graphQLClient;
    }

    private String LoadQueryFromResourcefile(String filename) throws IOException {
        String query = IOUtils.toString(ContentQuery.class.getClassLoader().getResourceAsStream("queries/"+filename + ".graphql"), "UTF-8");
        return query;
    }

    public <T> T GetPageModelData(Page page, Class<T> model) throws IOException {
        /*String query = graphQLQueries.LoadItems();

        dictionary.put("namespaceId", page.getNamespaceId());
        dictionary.put("publicationId", page.getPublicationId());
        dictionary.put("url", page.getUrl());

        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(dictionary);

        String contentQuery = _client.Execute(graphQLRequest);

        T pageModelData = objectMapper.readValue(contentQuery, model);

        return pageModelData;*/
        return null;
    }


    public <T> T ExecuteItemQuery(InputItemFilter filter, IPagination pagination) throws IOException {

        String customMetaFilter = "";
        String query = LoadQueryFromResourcefile("ItemQuery");
        query += LoadQueryFromResourcefile("ItemFieldsFragment");
        query += LoadQueryFromResourcefile("CustomMetaFieldsFragment");

        // We only include the fragments that will be required based on the item types in the
        // input item filter
        if (filter.getItemTypes() != null) {
            String fragmentList = "";
            for (ItemType itemType : filter.getItemTypes()) {
                String fragment = itemType.name().substring(0, 1).toUpperCase() + itemType.name().substring(1).toLowerCase() + "Fields";
                query += LoadQueryFromResourcefile(fragment + "Fragment");
                fragmentList += "..." + fragment + "\n";
            }
            // Just a quick and easy way to replace markers in our queries with vars here.
            query = query.replace("@fragmentList", fragmentList);
            query = query.replace("@customMetaFilter", "\"" + customMetaFilter + "\"");
        }

        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        HashMap<String, Object> variables = new HashMap<String, Object>();
        variables.put("first", pagination.getFirst());
        variables.put("after", pagination.getAfter());
        variables.put("filter", filter);
        variables.put("contextData", inputClaimValues);

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        String contentQuery = _client.execute(graphQLRequest);

        ObjectMapper objectMapper = new ObjectMapper();

        contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);

        return (T) contentComponent;
    }

    public <T> T GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType, DataModelType modelType, PageInclusion pageInclusion, boolean renderContent, IContextData contextData) {

        try {
            UpdateContextData(contextData, contentType, modelType, pageInclusion);
            String query="";
            try {
                query = LoadQueryFromResourcefile("PageModelById");
            }
            catch(Exception e){
                String s = e.getMessage();
            }
            HashMap<String, Object> variables = new HashMap<String, Object>();
            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);
            variables.put("pageId", pageId);
            variables.put("contextData", contextData);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            String contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);

        }catch (Exception ex){ex.printStackTrace();}
        return  (T) contentComponent;
    }

    protected void UpdateContextData(IContextData contextData, ContentType contentType, DataModelType dataModelType, PageInclusion pageInclusion) {
        if(contextData == null){
            contextData = new ContextData();
        }
    }


    public <T> T ExecuteSiteMap(Page page, Class<T> model) throws IOException {
        String query = LoadQueryFromResourcefile("GetSitemap");

        HashMap<String, Object> variables = new HashMap<String, Object>();
        variables.put("namespaceId", page.getNamespaceId());
        variables.put("publicationId", page.getPublicationId());

        GraphQLRequest graphQLRequest = new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

        String contentQuery = _client.execute(graphQLRequest);

        ObjectMapper objectMapper = new ObjectMapper();
        ObjectReader objectReader = objectMapper.reader(model).withRootName("data");

        T sitemap = objectReader.readValue(contentQuery);

        return sitemap;
    }

    public <T> T GetSitemap(ContentNamespace ns, int publicationId){

        try {
            String query = LoadQueryFromResourcefile("Sitemap");
            query += LoadQueryFromResourcefile("RecurseItems");
            query += LoadQueryFromResourcefile("TaxonomyItemFields");
            query += LoadQueryFromResourcefile("TaxonomyPageFields");

            HashMap<String, Object> variables = new HashMap<String, Object>();
            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            String contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);

        }catch (Exception ex){ex.printStackTrace();}
        return (T) contentComponent;
    }

    public <T> T GetSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId, boolean includeAncestors){
        try {
            String query = LoadQueryFromResourcefile("SitemapSubtree");
            query += LoadQueryFromResourcefile("TaxonomyItemFields");
            query += LoadQueryFromResourcefile("RecurseItems");
            query += LoadQueryFromResourcefile("TaxonomyPageFields");

            HashMap<String, Object> variables = new HashMap<String, Object>();
            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);
            variables.put("taxonomyNodeId", taxonomyNodeId);
            variables.put("includeAncestors", includeAncestors);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            String contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);

        }catch (Exception ex){ex.printStackTrace();}
        return (T) contentComponent;
    }

    public <T> T GetEntityModelData(ContentNamespace ns, int publicationId, int entityId){
        try {
            String query = LoadQueryFromResourcefile("EntityModelById");

            HashMap<String, Object> variables = new HashMap<String, Object>();

            variables.put("namespaceId", 1);
            variables.put("publicationId", publicationId);
            variables.put("entityId", entityId);

            GraphQLRequest graphQLRequest = new GraphQLRequest();
            graphQLRequest.setQuery(query);
            graphQLRequest.setVariables(variables);

            String contentQuery = _client.execute(graphQLRequest);

            ObjectMapper objectMapper = new ObjectMapper();
            contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);
        }catch (Exception ex){ex.printStackTrace();}
        return (T) contentComponent;
    }
}
