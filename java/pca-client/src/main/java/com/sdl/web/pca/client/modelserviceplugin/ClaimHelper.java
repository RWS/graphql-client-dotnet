package com.sdl.web.pca.client.modelserviceplugin;

import com.sdl.web.pca.client.contentmodel.ClaimValue;
import com.sdl.web.pca.client.contentmodel.ClaimValueType;
import com.sdl.web.pca.client.contentmodel.ContentType;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.enums.DataModelType;
import com.sdl.web.pca.client.contentmodel.enums.DcpType;
import com.sdl.web.pca.client.contentmodel.enums.PageInclusion;

import java.util.Arrays;
import java.util.List;

public class ClaimHelper {

    public static ClaimValue createClaim(ContentType contentType) {
        return new ClaimValue(ClaimValueType.STRING, ModelServiceClaimUris.CONTENT_TYPE, contentType.name());
    }

    public static ClaimValue createClaim(DataModelType dataModelType) {
        return new ClaimValue(ClaimValueType.STRING, ModelServiceClaimUris.MODEL_TYPE, dataModelType.name());
    }

    public static ClaimValue createClaim(PageInclusion pageInclusion) {
        return new ClaimValue(ClaimValueType.STRING, ModelServiceClaimUris.PAGE_INCLUDE_REGIONS, pageInclusion.name());
    }

    public static ClaimValue createClaim(DcpType dcpType) {
        return new ClaimValue(ClaimValueType.STRING, ModelServiceClaimUris.ENTITY_DCP_TYPE, dcpType.name());
    }

    public static ContextData createContextData(ContentType contentType, DataModelType modelType, PageInclusion pageInclusion) {
        ContextData data = new ContextData();
        List<ClaimValue> list = Arrays.asList(createClaim(contentType), createClaim(modelType), createClaim(pageInclusion));
        data.setClaimValues(list);
        return data;
    }
}
