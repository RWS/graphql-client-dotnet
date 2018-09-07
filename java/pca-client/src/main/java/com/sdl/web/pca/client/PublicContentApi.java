package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.generated.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.generated.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputPublicationFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputSortParam;
import com.sdl.web.pca.client.contentmodel.generated.ItemConnection;
import com.sdl.web.pca.client.contentmodel.generated.Publication;
import com.sdl.web.pca.client.contentmodel.generated.PublicationConnection;
import com.sdl.web.pca.client.contentmodel.generated.PublicationMapping;
import com.sdl.web.pca.client.contentmodel.generated.TaxonomySitemapItem;
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

    ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, Pagination pagination,
                                    ContextData contextData, String customMetaFilter,
                                    boolean renderContent) throws PublicContentApiException;

    Publication getPublication(ContentNamespace ns, int publicationId, ContextData contextData,
                               String customMetaFilter) throws PublicContentApiException;

    PublicationConnection getPublications(ContentNamespace ns, Pagination pagination, InputPublicationFilter filter,
                                          ContextData contextData, String customMetaFilter);

    String resolvePageLink(ContentNamespace ns, int publicationId, int pageId, boolean renderRelativeLink) throws PublicContentApiException;

    String resolveComponentLink(ContentNamespace ns, int publicationId, int componentId, Integer sourcePageId,
                                Integer excludeComponentTemplateId, boolean renderRelativeLink) throws PublicContentApiException;

    String resolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
                             String variantId, boolean renderRelativeLink) throws PublicContentApiException;

    String resolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
                                       int templateId, boolean renderRelativeLink) throws PublicContentApiException;

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
