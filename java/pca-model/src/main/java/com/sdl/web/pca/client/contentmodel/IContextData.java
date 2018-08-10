package com.sdl.web.pca.client.contentmodel;


import java.util.List;

/// Context Data
public interface IContextData {
    /// List of claim values to pass to query.

    List<ClaimValue> ClaimValues = null;

    List<ClaimValue> getClaimValues();
    void setClaimValues(List<ClaimValue> ClaimValues);
}
