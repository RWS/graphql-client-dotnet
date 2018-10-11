package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.ContentIncludeMode;
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
import com.sdl.web.pca.client.contentmodel.generated.Page;
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


    Page getPage(ContentNamespace ns, int publicationId, int pageId, String customMetaFilter,
                 ContentIncludeMode contentIncludeMode, ContextData contextData);

    Page getPage(ContentNamespace ns, int publicationId, String url, String customMetaFilter,
                 ContentIncludeMode contentIncludeMode, ContextData contextData);

    Page getPage(ContentNamespace ns, int publicationId, CmUri cmUri, String customMetaFilter,
                 ContentIncludeMode contentIncludeMode, ContextData contextData);

    /**
     * Retrieves BinaryComponent by providing publication id and binary id.
     *
     * @param ns            namespace
     * @param publicationId publication Id
     * @param binaryId      binary Id
     * @param contextData   context data
     * @return BinaryComponent representation
     * @throws PublicContentApiException
     */
    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
                                       ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves BinaryComponent by providing publication id and url of its binary component.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param url           binary component url
     * @param contextData   context data
     * @return BinaryComponent representation
     * @throws PublicContentApiException
     */
    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url,
                                       ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves BinaryConponent representation by providing its CMURI.
     *
     * @param cmUri       CMURI of binary component
     * @param contextData context data
     * @return BinaryComponent representation
     * @throws PublicContentApiException
     */
    BinaryComponent getBinaryComponent(CmUri cmUri, ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves a data structure which holds list of {@code Item} implementation by providing filtering parameters.
     *
     * @param filter           specifies filtering parameters for item query
     * @param sort             defines sorting order
     * @param pagination       defines pagination parameter
     * @param contextData      context data
     * @param customMetaFilter custom metadata filter
     * @param renderContent    specifies if content should be rendered
     * @return ItemConnection object which holds Items
     * @throws PublicContentApiException
     */
    ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, Pagination pagination,
                                    ContextData contextData, String customMetaFilter,
                                    boolean renderContent) throws PublicContentApiException;

    /**
     * Retrieves Publication by providing publication Id.
     *
     * @param ns               namespace
     * @param publicationId    publication id
     * @param contextData      context data
     * @param customMetaFilter custom metadata filter
     * @return Publication representation
     * @throws PublicContentApiException
     */
    Publication getPublication(ContentNamespace ns, int publicationId, ContextData contextData,
                               String customMetaFilter) throws PublicContentApiException;

    /**
     * Retrieves PublicationConnection object by given filter and pagination parameters.
     *
     * @param ns               namespace
     * @param pagination       defines pagination parameter
     * @param filter           defines filtering parameter
     * @param contextData      context data
     * @param customMetaFilter custom metadata filter
     * @return PublicationConnection representation
     */
    PublicationConnection getPublications(ContentNamespace ns, Pagination pagination, InputPublicationFilter filter,
                                          ContextData contextData, String customMetaFilter);

    /**
     * Retrieves link to page by given publication and page id.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param pageId             page id
     * @param renderRelativeLink indicates if it is relative or full link
     * @return page link string
     * @throws PublicContentApiException
     */
    String resolvePageLink(ContentNamespace ns, int publicationId, int pageId, boolean renderRelativeLink) throws PublicContentApiException;

    /**
     * Retrieves link to component by given publication id and component id.
     *
     * @param ns                         namespace
     * @param publicationId              publication id
     * @param componentId                component id
     * @param sourcePageId               source page id
     * @param excludeComponentTemplateId exclude compoonent template id
     * @param renderRelativeLink         indicates if it is relative or full link
     * @return component link string
     * @throws PublicContentApiException
     */
    String resolveComponentLink(ContentNamespace ns, int publicationId, int componentId, Integer sourcePageId,
                                Integer excludeComponentTemplateId, boolean renderRelativeLink) throws PublicContentApiException;

    /**
     * Retrieves link to binary component by given publication id and binary id.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param binaryId           binary id
     * @param variantId          variant id
     * @param renderRelativeLink indicates if it is relative or full link
     * @return binary link string
     * @throws PublicContentApiException
     */
    String resolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
                             String variantId, boolean renderRelativeLink) throws PublicContentApiException;

    /**
     * Retrieves link to dynamic component by given publication, page, component and template ids.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param pageId             page id
     * @param componentId        component id
     * @param templateId         template id
     * @param renderRelativeLink indicates if it is relative or full link
     * @return dynamic component link string
     * @throws PublicContentApiException
     */
    String resolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
                                       int templateId, boolean renderRelativeLink) throws PublicContentApiException;

    /**
     * Retrieves publication mapping by given url.
     *
     * @param ns  namespace
     * @param url url
     * @return PublicationMapping representation
     * @throws PublicContentApiException
     */
    PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws PublicContentApiException;

    /**
     * Retrieves page model data by given publication id and url.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param url           url
     * @param contentType   content type
     * @param modelType     data model type
     * @param pageInclusion page inclusion
     * @param renderContent indicates if content should be rendered
     * @param contextData   context data
     * @return json representation of page model
     * @throws PublicContentApiException
     */
    JsonNode getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                              DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                              ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves page model data by given publication id and page id.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param pageId        page id
     * @param contentType   content type
     * @param modelType     data model type
     * @param pageInclusion page inclusion
     * @param renderContent indicates if content should be rendered
     * @param contextData   context data
     * @return json representation of page model
     * @throws PublicContentApiException
     */
    JsonNode getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                              DataModelType modelType, PageInclusion pageInclusion, boolean renderContent,
                              ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves entity model data by given publication, entity and template ids.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param entityId      entity id
     * @param templateId    template id
     * @param contentType   content type
     * @param modelType     model type
     * @param dcpType       dcp type
     * @param renderContent indicates if content should be rendered
     * @param contextData   context data
     * @return json representation of entity model
     * @throws PublicContentApiException
     */
    JsonNode getEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
                                ContentType contentType, DataModelType modelType, DcpType dcpType,
                                boolean renderContent, ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves TaxonomySitemapItem by given publication id.
     *
     * @param ns               namespace
     * @param publicationId    publication id
     * @param descendantLevels descendant level
     * @param contextData      context data
     * @return TaxonomySitemapItem representation
     * @throws PublicContentApiException
     */
    TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                   ContextData contextData) throws PublicContentApiException;

    /**
     * Retrieves TaxonomySitemapItem array subtree by given publication id and parent taxonomy node id.
     *
     * @param ns               namespace
     * @param publicationId    publication id
     * @param taxonomyNodeId   taxonomy node id
     * @param descendantLevels descendant levels
     * @param includeAncestors indicates if ancestors will be included
     * @param contextData      context data
     * @return TaxonomySitemapItem array representation
     * @throws PublicContentApiException
     */
    TaxonomySitemapItem[] getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                            int descendantLevels, boolean includeAncestors,
                                            ContextData contextData) throws PublicContentApiException;

}
