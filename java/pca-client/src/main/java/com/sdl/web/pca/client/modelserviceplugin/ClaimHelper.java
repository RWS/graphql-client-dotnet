package com.sdl.web.pca.client.modelserviceplugin;

import com.sdl.web.pca.client.contentmodel.enums.ContentType;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.ModelServiceLinkRendering;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;
import com.sdl.web.pca.client.contentmodel.enums.TcdlLinkRendering;
import com.sdl.web.pca.client.contentmodel.generated.ClaimValue;
import com.sdl.web.pca.client.contentmodel.generated.ClaimValueType;

public class ClaimHelper {

    public static ClaimValue createClaim(ModelServiceLinkRendering linkRendering) {
        return createClaimInternal(ModelServiceClaimUris.MODEL_SERVICE_LINK_RENDERING,
                linkRendering == ModelServiceLinkRendering.RELATIVE ? "true" : "false",
                ClaimValueType.BOOLEAN);
    }

    public static ClaimValue createClaim(TcdlLinkRendering linkRendering) {
        return createClaimInternal(ModelServiceClaimUris.TCDL_LINK_RENDERING,
                linkRendering == TcdlLinkRendering.RELATIVE ? "true" : "false",
                ClaimValueType.BOOLEAN);
    }

    public static ClaimValue createClaimTcdlLinkUrlPrefix(String urlPrefix) {
        return createClaimInternal(ModelServiceClaimUris.TCDL_LINK_URL_PREFIX,
                urlPrefix,
                ClaimValueType.STRING);
    }

    public static ClaimValue createClaimTcdlBinaryLinkUrlPrefix(String urlPrefix) {
        return createClaimInternal(ModelServiceClaimUris.TCDL_BINARY_LINK_URL_PREFIX,
                urlPrefix,
                ClaimValueType.STRING);
    }

    public static ClaimValue createClaim(ContentType contentType) {
        return createClaimInternal(ModelServiceClaimUris.CONTENT_TYPE,
                contentType.name(),
                ClaimValueType.STRING);
    }

    public static ClaimValue createClaim(DataModelType dataModelType) {
        return createClaimInternal(ModelServiceClaimUris.MODEL_TYPE,
                dataModelType.name(),
                ClaimValueType.STRING);
    }

    public static ClaimValue createClaim(PageInclusion pageInclusion) {
        return createClaimInternal(ModelServiceClaimUris.PAGE_INCLUDE_REGIONS,
                pageInclusion.name(),
                ClaimValueType.STRING);
    }

    public static ClaimValue createClaim(DcpType dcpType) {
        return createClaimInternal(ModelServiceClaimUris.ENTITY_DCP_TYPE,
                dcpType.name(),
                ClaimValueType.STRING);
    }

    private static ClaimValue createClaimInternal(String uri, String value, ClaimValueType type) {
        ClaimValue claim = new ClaimValue();
        claim.setType(type);
        claim.setUri(uri);
        claim.setValue(value);
        return claim;
    }
}
