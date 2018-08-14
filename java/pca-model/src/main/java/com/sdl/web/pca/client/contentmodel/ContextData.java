package com.sdl.web.pca.client.contentmodel;

import java.util.List;

public class ContextData implements IContextData {

    private List<ClaimValue> ClaimValues;

    @Override
    public List<ClaimValue> getClaimValues() {
        return ClaimValues;
    }

    @Override
    public void setClaimValues(List<ClaimValue> claimValues) {
        ClaimValues = claimValues;
    }
}
