package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.generated.ClaimValue;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;

public class ContextData {
    private List<ClaimValue> claimValues = new ArrayList<>();

    public List<ClaimValue> getClaimValues() {
        return Collections.unmodifiableList(claimValues);
    }

    public void addClaimValule(ClaimValue value) {
        claimValues.add(value);
    }

    public void addClaimValues(ContextData values) {
        if (values != null && values.getClaimValues() != null) {
            claimValues.addAll(values.getClaimValues());
        }
    }

    public void addClaimValues(Collection<ClaimValue> values) {
        if (values != null) {
            claimValues.addAll(values);
        }
    }
}
