package com.sdl.web.pca.client.modelserviceplugin;

import com.sdl.web.pca.client.contentmodel.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.ContentType;
import com.sdl.web.pca.client.contentmodel.IContextData;
import com.sdl.web.pca.client.contentmodel.TaxonomySitemapItem;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.exception.PublicContentApiException;

/**
 * Model service plugin API.
 */
//TODO: update javadoc with meaningful values
public interface ModelServicePluginApi {

    /**
     * @param ns
     * @param publicationId
     * @param url
     * @param contentType
     * @param modelType
     * @param pageInclusion
     * @param renderContent
     * @param contextData
     * @param <T>
     * @return
     * @throws PublicContentApiException
     */
    <T> T getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                           DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                           IContextData contextData, Class<T> clazz) throws PublicContentApiException;

    /**
     * @param ns
     * @param publicationId
     * @param pageId
     * @param contentType
     * @param modelType
     * @param pageInclusion
     * @param renderContent
     * @param contextData
     * @param clazz
     * @param <T>
     * @return
     * @throws PublicContentApiException
     */
    <T> T getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                           DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                           IContextData contextData, Class<T> clazz) throws PublicContentApiException;

    /**
     * @param ns
     * @param publicationId
     * @param entityId
     * @param contentType
     * @param modelType
     * @param dcpType
     * @param renderContent
     * @param contextData
     * @param <T>
     * @return
     * @throws PublicContentApiException
     */
    <T> T getEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
                             DataModelType modelType, DcpType dcpType, boolean renderContent,
                             IContextData contextData, Class<T> clazz) throws PublicContentApiException;

    /**
     * @param ns
     * @param publicationId
     * @param descendantLevels
     * @param contextData
     * @return
     * @throws PublicContentApiException
     */
    TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                   IContextData contextData) throws PublicContentApiException;

    /**
     * @param ns
     * @param publicationId
     * @param taxonomyNodeId
     * @param descendantLevels
     * @param includeAncestors
     * @param contextData
     * @return
     * @throws PublicContentApiException
     */
    TaxonomySitemapItem getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                          int descendantLevels, boolean includeAncestors,
                                          IContextData contextData) throws PublicContentApiException;
}
