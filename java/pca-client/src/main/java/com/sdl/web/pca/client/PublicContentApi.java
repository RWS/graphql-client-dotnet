package com.sdl.web.pca.client;

import com.sdl.web.pca.client.contentmodel.BinaryComponent;
import com.sdl.web.pca.client.contentmodel.ContentNamespace;
import com.sdl.web.pca.client.contentmodel.IContextData;
import com.sdl.web.pca.client.contentmodel.IPagination;
import com.sdl.web.pca.client.contentmodel.InputItemFilter;
import com.sdl.web.pca.client.contentmodel.InputSortParam;
import com.sdl.web.pca.client.contentmodel.ItemConnection;
import com.sdl.web.pca.client.contentmodel.Publication;
import com.sdl.web.pca.client.contentmodel.PublicationMapping;
import com.sdl.web.pca.client.exception.PublicContentApiException;

/**
 * This interface enables java clients to connect to the GraphQL Service
 */
public interface PublicContentApi {

    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
                                       IContextData contextData) throws PublicContentApiException;

    BinaryComponent getBinaryComponent(ContentNamespace ns, int publicationId, String url,
                                       IContextData contextData) throws PublicContentApiException;

    BinaryComponent getBinaryComponent(CmUri cmUri, IContextData contextData) throws PublicContentApiException;

    ItemConnection executeItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
                                    IContextData contextData, String customMetaFilter,
                                    boolean renderContent) throws PublicContentApiException;

    Publication getPublication(ContentNamespace ns, int publicationId, IContextData contextData,
                               String customMetaFilter) throws PublicContentApiException;

    String resolveLink(CmUri cmUri, boolean resolveToBinary) throws PublicContentApiException;

    PublicationMapping getPublicationMapping(ContentNamespace ns, String url) throws PublicContentApiException;
}
