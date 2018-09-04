package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.sdl.web.pca.client.contentmodel.*;
import com.sdl.web.pca.client.contentmodel.enums.*;
import com.sdl.web.pca.client.exception.PublicContentApiException;
import com.sdl.web.pca.client.util.CmUri;

/**
 * This interface enables java clients to connect to the GraphQL Service
 */
public interface PublicContentApi {
    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
                                       ContextData contextData) throws PublicContentApiException;

    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url,
                                       ContextData contextData) throws PublicContentApiException;

    BinaryComponent getBinaryComponent(CmUri cmUri, ContextData contextData) throws PublicContentApiException;

    ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
                                    ContextData contextData, String customMetaFilter,
                                    boolean renderContent) throws PublicContentApiException;

    Publication getPublication(ContentNamespace ns, int publicationId, ContextData contextData,
                               String customMetaFilter) throws PublicContentApiException;

    PublicationConnection getPublications(ContentNamespace ns, IPagination pagination, InputPublicationFilter filter,
                                          IContextData contextData, String customMetaFilter);

    String ResolvePageLink(ContentNamespace ns, int publicationId, int pageId) throws PublicContentApiException;

    String ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, Integer sourcePageId,
                                Integer excludeComponentTemplateId) throws PublicContentApiException;

    String ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
                             String variantId) throws PublicContentApiException;

    String ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
                                       int templateId) throws PublicContentApiException;

    PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws PublicContentApiException;

    JsonNode getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                              DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                              ContextData contextData) throws PublicContentApiException;


    JsonNode getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                              DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                              ContextData contextData) throws PublicContentApiException;

    JsonNode getEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
                                ContentType contentType, DataModelType modelType, DcpType dcpType,
                                boolean renderContent, ContextData contextData) throws PublicContentApiException;

    TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                   ContextData contextData) throws PublicContentApiException;

    TaxonomySitemapItem getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                          int descendantLevels, boolean includeAncestors,
                                          ContextData contextData) throws PublicContentApiException;

}
