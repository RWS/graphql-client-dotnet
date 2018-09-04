package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.generated.ClaimValue;

import java.util.List;

public class ContextData {

    private List<ClaimValue> ClaimValues;

    public List<ClaimValue> getClaimValues() {
        return ClaimValues;
    }

    public void setClaimValues(List<ClaimValue> claimValues) {
        ClaimValues = claimValues;
    }
}
