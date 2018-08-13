package com.sdl.web.pca.client;

import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;

import java.io.IOException;

/**
 * This interface enables java clients to connect to the GraphQL Service
 * @author
 * @version 1.0.0
 */
public interface IPublicContentApi {

    /**
     * This method can be used to execute the graphQL PageModelById querie.
     * @param ns specify the Content namespace
     * @param publicationId  specify the publication Id.
     * @param pageId specify the page Id.
     * @param contentType specify the content type.
     * @param modelType specify the model type.
     * @param pageInclusion specify the page inclusion.
     * @param renderContent specify the render content.
     * @param contextData specify the context data.
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType, DataModelType modelType, PageInclusion pageInclusion, boolean renderContent, IContextData contextData);

    /**
     * This method can be used to execute the graphQL ItemQuery querie using InputItemFilter & Pagination parameters.
     * @param filter
     * @param pagination
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T ExecuteItemQuery(InputItemFilter filter, IPagination pagination) throws IOException;

    /**
     * This method can be used to execute the graphQL Sitemap querie using Page & Class model parameters.
     * @param page
     * @param model
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T ExecuteSiteMap(Page page, Class<T> model) throws IOException;

    /**
     * This method can be used to execute the graphQL Sitemap querie using ContentNamespace & publicationId parameters.
     * @param ns
     * @param publicationId
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T GetSitemap(ContentNamespace ns, int publicationId);

    /**
     * This method can be used to execute the graphQL Sitemap querie using ContentNamespace, publicationId, taxonomyNodeId & includeAncestors parameters.
     * @param ns
     * @param publicationId
     * @param taxonomyNodeId
     * @param includeAncestors
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T GetSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId, boolean includeAncestors);

    /**
     * This method can be used to execute the graphQL EntityModelById querie using ContentNamespace, publicationId & entityId parameters.
     * @param ns
     * @param publicationId
     * @param entityId
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T GetEntityModelData(ContentNamespace ns, int publicationId, int entityId);
}
