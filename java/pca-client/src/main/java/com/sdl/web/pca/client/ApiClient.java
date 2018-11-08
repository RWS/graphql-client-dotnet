package com.sdl.web.pca.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.Pagination;
import com.sdl.web.pca.client.contentmodel.enums.ContentIncludeMode;
import com.sdl.web.pca.client.contentmodel.enums.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.ModelServiceLinkRendering;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.enums.TcdlLinkRendering;
import com.sdl.web.pca.client.contentmodel.generated.Ancestor;
import com.sdl.web.pca.client.contentmodel.generated.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.generated.ComponentPresentation;
import com.sdl.web.pca.client.contentmodel.generated.ComponentPresentationConnection;
import com.sdl.web.pca.client.contentmodel.generated.InputComponentPresentationFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputPublicationFilter;
import com.sdl.web.pca.client.contentmodel.generated.InputSortParam;
import com.sdl.web.pca.client.contentmodel.generated.ItemConnection;
import com.sdl.web.pca.client.contentmodel.generated.Page;
import com.sdl.web.pca.client.contentmodel.generated.PageConnection;
import com.sdl.web.pca.client.contentmodel.generated.Publication;
import com.sdl.web.pca.client.contentmodel.generated.PublicationConnection;
import com.sdl.web.pca.client.contentmodel.generated.PublicationMapping;
import com.sdl.web.pca.client.contentmodel.generated.TaxonomySitemapItem;
import com.sdl.web.pca.client.exception.ApiClientException;
import com.sdl.web.pca.client.util.CmUri;

/**
 * This interface enables java clients to connect to the GraphQL Service.
 */
public interface ApiClient {

    /**
     * Holds global context data passed on to PCA service. Note that context data passed
     * directly to API methods overwrites these values.
     *
     * @return global context data
     */
    ContextData getGlobalContextData();

    /**
     * Sets global context data.
     *
     * @param globalContextData
     */
    void setGlobalContextData(ContextData globalContextData);

    /**
     * Returns default content type.
     *
     * @return default content type
     */
    ContentType getDefaultContentType();

    /**
     * Specify type of content to return from API. When set to RAW no conversion will take
     * place otherwise its treated as model data and will go through conversion to type specified
     * by DefaultModelType (default: MODEL).
     *
     * @param contentType
     */
    void setDefaultContentType(ContentType contentType);

    /**
     * Retrurns default model type.
     *
     * @return
     */
    DataModelType getDefaultModelType();

    /**
     * Specify model type to return (default: R2).
     *
     * @param dataModelType
     */
    void setDefaultModelType(DataModelType dataModelType);

    /**
     * Returns tcdl link rendering type.
     *
     * @return
     */
    TcdlLinkRendering getTcdlLinkRenderingType();

    /**
     * Specify how tcdl links get rendered (default: RELATIVE).
     *
     * @param tcdlLinkRenderingType
     */
    void setTcdlLinkRenderingType(TcdlLinkRendering tcdlLinkRenderingType);

    /**
     * Returns model service link rendering type.
     *
     * @return
     */
    ModelServiceLinkRendering getModelSericeLinkRenderingType();

    /**
     * Specify how the model-service plugin renders links (default: RELATIVE).
     *
     * @param modelSericeLinkRenderingType
     */
    void setModelSericeLinkRenderingType(ModelServiceLinkRendering modelSericeLinkRenderingType);

    /**
     * Returns Url prefix for tcdl links.
     *
     * @return
     */
    String getTcdlLinkUrlPrefix();

    /**
     * Specify Url prefix for tcdl links for Absolute rendering type (default: null).
     *
     * @param tcdlLinkUrlPrefix
     */
    void setTcdlLinkUrlPrefix(String tcdlLinkUrlPrefix);

    /**
     * Returns Url prefix for tcdl binary links.
     *
     * @return
     */
    String getTcdlBinaryLinkUrlPrefix();

    /**
     * Specify Url prefix for tcdl binary links for Absolute rendering type (default: null).
     *
     * @param tcdlBinaryLinkUrlPrefix
     */
    void setTcdlBinaryLinkUrlPrefix(String tcdlBinaryLinkUrlPrefix);

    /**
     * Retrieves ComponentPresentation object by given namespace, publication id, component id and template id.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param componentId        component id
     * @param templateId         template id
     * @param customMetaFilter   custom meta filter
     * @param contentIncludeMode content include mode
     * @param contextData        context data
     * @return ComponentPresentation instance
     */
    ComponentPresentation getComponentPresentation(ContentNamespace ns, int publicationId, int componentId, int templateId,
                                                   String customMetaFilter, ContentIncludeMode contentIncludeMode,
                                                   ContextData contextData);

    /**
     * Retrieves ComponentPresentationConnection by given namespace, publication id and component presentation filter.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param filter             component presentation filter
     * @param sort               sorting
     * @param pagination         pagination
     * @param customMetaFilter   custom meta filter
     * @param contentIncludeMode content include mode
     * @param contextData        context data
     * @return ComponentPresentationConnection instance
     */
    ComponentPresentationConnection getComponentPresentations(ContentNamespace ns, int publicationId,
                                                              InputComponentPresentationFilter filter, InputSortParam sort,
                                                              Pagination pagination, String customMetaFilter,
                                                              ContentIncludeMode contentIncludeMode, ContextData contextData);

    /**
     * Retrieves Page representation by given namespace, publication id and page id.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param pageId             page id
     * @param customMetaFilter   custom meta filter
     * @param contentIncludeMode content include mode
     * @param contextData        context data
     * @return Page representation
     */
    Page getPage(ContentNamespace ns, int publicationId, int pageId, String customMetaFilter,
                 ContentIncludeMode contentIncludeMode, ContextData contextData);

    /**
     * Retrieves Page representation by given namespace, publication id and URL.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param url                page URL
     * @param customMetaFilter   custom meta filter
     * @param contentIncludeMode content include mode
     * @param contextData        context data
     * @return Page representation
     */
    Page getPage(ContentNamespace ns, int publicationId, String url, String customMetaFilter,
                 ContentIncludeMode contentIncludeMode, ContextData contextData);

    /**
     * Retrieves Page representation by fiven CmUri.
     *
     * @param cmUri              CmUri of the page
     * @param customMetaFilter   custom meta filter
     * @param contentIncludeMode content include mode
     * @param contextData        context data
     * @return Page representation
     */
    Page getPage(CmUri cmUri, String customMetaFilter,
                 ContentIncludeMode contentIncludeMode, ContextData contextData);

    /**
     * Retrieves PageConnection representation by given namespace, pagination and URL
     *
     * @param ns                 namespace
     * @param pagination         pagination parameter
     * @param url                page URL
     * @param customMetaFilter   custom meta filter
     * @param contentIncludeMode content include mode
     * @param contextData        context data
     * @return PageConnection representation
     */
    PageConnection getPages(ContentNamespace ns, Pagination pagination, String url, String customMetaFilter,
                            ContentIncludeMode contentIncludeMode, ContextData contextData);

    /**
     * Retrieves BinaryComponent by providing publication id and binary id.
     *
     * @param ns            namespace
     * @param publicationId publication Id
     * @param binaryId      binary Id
     * @param contextData   context data
     * @return BinaryComponent representation
     * @throws ApiClientException
     */
    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId, String customMetaFilter,
                                       ContextData contextData) throws ApiClientException;

    /**
     * Retrieves BinaryComponent by providing publication id and url of its binary component.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param url           binary component url
     * @param contextData   context data
     * @return BinaryComponent representation
     * @throws ApiClientException
     */
    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url, String customMetaFilter,
                                       ContextData contextData) throws ApiClientException;

    /**
     * Retrieves BinaryConponent representation by providing its CMURI.
     *
     * @param cmUri       CMURI of binary component
     * @param contextData context data
     * @return BinaryComponent representation
     * @throws ApiClientException
     */
    BinaryComponent getBinaryComponent(CmUri cmUri, String customMetaFilter, ContextData contextData) throws ApiClientException;

    /**
     * Retrieves a data structure which holds list of {@code Item} implementation by providing filtering parameters.
     *
     * @param filter           specifies filtering parameters for item query
     * @param sort             defines sorting order
     * @param pagination       defines pagination parameter
     * @param contextData      context data
     * @param customMetaFilter custom metadata filter
     * @return ItemConnection object which holds Items
     * @throws ApiClientException
     */
    ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, Pagination pagination,
                                    String customMetaFilter, ContentIncludeMode contentIncludeMode,
                                    boolean includeContainerItems, ContextData contextData) throws ApiClientException;

    /**
     * Retrieves Publication by providing publication Id.
     *
     * @param ns               namespace
     * @param publicationId    publication id
     * @param contextData      context data
     * @param customMetaFilter custom metadata filter
     * @return Publication representation
     * @throws ApiClientException
     */
    Publication getPublication(ContentNamespace ns, int publicationId, String customMetaFilter,
                               ContextData contextData) throws ApiClientException;

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
                                          String customMetaFilter,
                                          ContextData contextData);

    /**
     * Retrieves link to page by given publication and page id.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param pageId             page id
     * @param renderRelativeLink indicates if it is relative or full link
     * @return page link string
     * @throws ApiClientException
     */
    String resolvePageLink(ContentNamespace ns, int publicationId, int pageId, boolean renderRelativeLink) throws ApiClientException;

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
     * @throws ApiClientException
     */
    String resolveComponentLink(ContentNamespace ns, int publicationId, int componentId, Integer sourcePageId,
                                Integer excludeComponentTemplateId, boolean renderRelativeLink) throws ApiClientException;

    /**
     * Retrieves link to binary component by given publication id and binary id.
     *
     * @param ns                 namespace
     * @param publicationId      publication id
     * @param binaryId           binary id
     * @param variantId          variant id
     * @param renderRelativeLink indicates if it is relative or full link
     * @return binary link string
     * @throws ApiClientException
     */
    String resolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId, String variantId,
                             boolean renderRelativeLink) throws ApiClientException;

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
     * @throws ApiClientException
     */
    String resolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId, int componentId,
                                       int templateId, boolean renderRelativeLink) throws ApiClientException;

    /**
     * Retrieves publication mapping by given url.
     *
     * @param ns  namespace
     * @param url url
     * @return PublicationMapping representation
     * @throws ApiClientException
     */
    PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws ApiClientException;

    /**
     * Retrieves page model data by given publication id and url.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param url           url
     * @param contentType   content type
     * @param modelType     data model type
     * @param pageInclusion page inclusion
     * @param contextData   context data
     * @return json representation of page model
     * @throws ApiClientException
     */
    JsonNode getPageModelData(ContentNamespace ns, int publicationId, String url, ContentType contentType,
                              DataModelType modelType, PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode,
                              ContextData contextData) throws ApiClientException;

    /**
     * Retrieves page model data by given publication id and page id.
     *
     * @param ns            namespace
     * @param publicationId publication id
     * @param pageId        page id
     * @param contentType   content type
     * @param modelType     data model type
     * @param pageInclusion page inclusion
     * @param contextData   context data
     * @return json representation of page model
     * @throws ApiClientException
     */
    JsonNode getPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
                              DataModelType modelType, PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode,
                              ContextData contextData) throws ApiClientException;

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
     * @param contextData   context data
     * @return json representation of entity model
     * @throws ApiClientException
     */
    JsonNode getEntityModelData(ContentNamespace ns, int publicationId, int entityId, int templateId,
                                ContentType contentType,
                                DataModelType modelType, DcpType dcpType, ContentIncludeMode contentIncludeMode,
                                ContextData contextData) throws ApiClientException;

    /**
     * Retrieves TaxonomySitemapItem by given publication id.
     *
     * @param ns               namespace
     * @param publicationId    publication id
     * @param descendantLevels descendant level
     * @param contextData      context data
     * @return TaxonomySitemapItem representation
     * @throws ApiClientException
     */
    TaxonomySitemapItem getSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
                                   ContextData contextData) throws ApiClientException;

    /**
     * Retrieves TaxonomySitemapItem array subtree by given publication id and parent taxonomy node id.
     *
     * @param ns               namespace
     * @param publicationId    publication id
     * @param taxonomyNodeId   taxonomy node id
     * @param descendantLevels descendant levels
     * @param contextData      context data
     * @return TaxonomySitemapItem array representation
     * @throws ApiClientException
     */
    TaxonomySitemapItem[] getSitemapSubtree(ContentNamespace ns, int publicationId, String taxonomyNodeId,
                                            int descendantLevels, Ancestor ancestor,
                                            ContextData contextData) throws ApiClientException;

}
