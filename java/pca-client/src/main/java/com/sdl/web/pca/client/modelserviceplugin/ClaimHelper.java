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
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.BOOLEAN);
        value.setUri(ModelServiceClaimUris.TCDL_LINK_URL_PREFIX);
        value.setValue(linkRendering == ModelServiceLinkRendering.RELATIVE ? "true" : "false");
        return value;
    }

    public static ClaimValue createClaim(TcdlLinkRendering linkRendering) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.BOOLEAN);
        value.setUri(ModelServiceClaimUris.TCDL_LINK_URL_PREFIX);
        value.setValue(linkRendering == TcdlLinkRendering.RELATIVE ? "true" : "false");
        return value;
    }

    public static ClaimValue createClaimTcdlLinkUrlPrefix(String urlPrefix) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.STRING);
        value.setUri(ModelServiceClaimUris.TCDL_LINK_URL_PREFIX);
        value.setValue(urlPrefix);
        return value;
    }

    public static ClaimValue createClaimTcdlBinaryLinkUrlPrefix(String urlPrefix) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.STRING);
        value.setUri(ModelServiceClaimUris.TCDL_BINARY_LINK_URL_PREFIX);
        value.setValue(urlPrefix);
        return value;
    }

    public static ClaimValue createClaim(ContentType contentType) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.STRING);
        value.setUri(ModelServiceClaimUris.CONTENT_TYPE);
        value.setValue(contentType.name());
        return value;
    }

    public static ClaimValue createClaim(DataModelType dataModelType) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.STRING);
        value.setUri(ModelServiceClaimUris.MODEL_TYPE);
        value.setValue(dataModelType.name());
        return value;
    }

    public static ClaimValue createClaim(PageInclusion pageInclusion) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.STRING);
        value.setUri(ModelServiceClaimUris.PAGE_INCLUDE_REGIONS);
        value.setValue(pageInclusion.name());
        return value;
    }

    public static ClaimValue createClaim(DcpType dcpType) {
        ClaimValue value = new ClaimValue();
        value.setType(ClaimValueType.STRING);
        value.setUri(ModelServiceClaimUris.ENTITY_DCP_TYPE);
        value.setValue(dcpType.name());
        return value;
    }
}
