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
        ClaimValue cValue = new ClaimValue();
        cValue.setType(ClaimValueType.STRING);
        cValue.setUri(ModelServiceClaimUris.CONTENT_TYPE);
        cValue.setValue(contentType.name());
        return cValue;
    }

    public static ClaimValue createClaim(DataModelType dataModelType) {
        ClaimValue cValue = new ClaimValue();
        cValue.setType(ClaimValueType.STRING);
        cValue.setUri(ModelServiceClaimUris.MODEL_TYPE);
        cValue.setValue(dataModelType.name());
        return cValue;
    }

    public static ClaimValue createClaim(PageInclusion pageInclusion) {
        ClaimValue cValue = new ClaimValue();
        cValue.setType(ClaimValueType.STRING);
        cValue.setUri(ModelServiceClaimUris.PAGE_INCLUDE_REGIONS);
        cValue.setValue(pageInclusion.name());
        return cValue;
    }

    public static ClaimValue createClaim(DcpType dcpType) {
        ClaimValue cValue = new ClaimValue();
        cValue.setType(ClaimValueType.STRING);
        cValue.setUri(ModelServiceClaimUris.ENTITY_DCP_TYPE);
        cValue.setValue(dcpType.name());
        return cValue;
    }

    public static ContextData createContextData(ContentType contentType, DataModelType modelType, PageInclusion pageInclusion) {
        ContextData data = new ContextData();
        List<ClaimValue> list = Arrays.asList(createClaim(contentType), createClaim(modelType), createClaim(pageInclusion));
        data.setClaimValues(list);
        return data;
    }
}
