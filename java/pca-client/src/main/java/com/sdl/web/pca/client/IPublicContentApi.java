package com.sdl.web.pca.client;

import com.sdl.web.pca.client.contentmodel.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.ContentType;
import com.sdl.web.pca.client.contentmodel.IContextData;
import com.sdl.web.pca.client.contentmodel.IPagination;
import com.sdl.web.pca.client.contentmodel.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.Page;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.exception.PublicContentApiException;

/**
 * This interface enables java clients to connect to the GraphQL Service
 */
public interface IPublicContentApi {

    /**
     * This method can be used to execute the graphQL PageModelById querie.
     *
     * @param ns            specify the Content namespace
     * @param publicationId specify the publication Id.
     * @param pageId        specify the page Id.
     * @param contentType   specify the content type.
     * @param modelType     specify the model type.
     * @param pageInclusion specify the page inclusion.
     * @param renderContent specify the render content.
     * @param contextData   specify the context data.
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType, DataModelType modelType, PageInclusion pageInclusion, boolean renderContent, IContextData contextData, Class<T> clazz) throws PublicContentApiException;

    /**
     * This method can be used to execute the graphQL ItemQuery querie using InputItemFilter & Pagination parameters.
     *
     * @param filter
     * @param pagination
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T executeItemQuery(InputItemFilter filter, IPagination pagination, Class<T> clazz) throws PublicContentApiException;

    /**
     * This method can be used to execute the graphQL Sitemap querie using Page & Class model parameters.
     *
     * @param page
     * @param model
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T executeSiteMap(Page page, Class<T> model) throws PublicContentApiException;

    /**
     * This method can be used to execute the graphQL Sitemap querie using ContentNamespace & publicationId parameters.
     *
     * @param ns
     * @param publicationId
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T getSitemap(ContentNamespace ns, int publicationId, Class<T> clazz) throws PublicContentApiException;

    /**
     * This method can be used to execute the graphQL Sitemap querie using ContentNamespace, publicationId, taxonomyNodeId & includeAncestors parameters.
     *
     * @param ns
     * @param publicationId
     * @param taxonomyNodeId
     * @param includeAncestors
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId, boolean includeAncestors, Class<T> clazz) throws PublicContentApiException;

    /**
     * This method can be used to execute the graphQL EntityModelById querie using ContentNamespace, publicationId & entityId parameters.
     *
     * @param ns
     * @param publicationId
     * @param entityId
     * @return The GraphQL JSON string response with data and errors if any.
     */
    <T> T getEntityModelData(ContentNamespace ns, int publicationId, int entityId, Class<T> clazz) throws PublicContentApiException;
}
