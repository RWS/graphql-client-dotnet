package com.sdl.web.pca.client.query;

import com.google.common.base.Strings;
import com.sdl.web.pca.client.contentmodel.ContextData;
import com.sdl.web.pca.client.contentmodel.enums.ContentIncludeMode;
import com.sdl.web.pca.client.contentmodel.generated.ClaimValue;
import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.util.QueryUtils;

import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static java.util.regex.Pattern.DOTALL;
import static java.util.regex.Pattern.MULTILINE;

/**
 * Builder for Public Content API grpaphql request
 */
public class PCARequestBuilder {
    private static final Pattern FRAGMENT_NAMES_FROM_BODY = Pattern.compile("^\\s*[.]{3}(?<fragmentName>\\w*)\\s*$",
            DOTALL | MULTILINE);

    private String query;
    private String queryName;
    private Set<String> injectFragments = new HashSet<>();
    private Map<String, Boolean> includeRegions = new HashMap<>();
    private String recurseFragmentName;
    private int descendantLevel = 0;
    private Map<String, Object> variables = new HashMap<>();
    private String operationName;
    private ContextData contextData = new ContextData();
    private int timeout;
    private QueryHolder queryHolder = QueryHolder.getInstance();
    private String variantArgs;
    private String customMetaFilter;
    private ContentIncludeMode contentIncludeMode = ContentIncludeMode.EXCLUDE;

    public PCARequestBuilder() {
    }

    /**
     * Loads query by given query name.
     *
     * @param queryName query name
     * @return
     */
    public PCARequestBuilder withQuery(String queryName) {
        this.queryName = queryName;
        return this;
    }


    /**
     * Updates placeholder '@fragmentList' with the given list of fragments.
     *
     * @param injectFragments list of fragments
     * @return
     */
    public PCARequestBuilder withInjectFragments(List<String> injectFragments) {
        if (injectFragments != null) {
            this.injectFragments.addAll(injectFragments);
        }
        return this;
    }

    /**
     * Loads recurse fragment and applies it to query with given descendant level
     *
     * @param recurseFragmentName
     * @param descendantLevel
     * @return
     */
    public PCARequestBuilder withRecurseFragment(String recurseFragmentName, int descendantLevel) {
        this.recurseFragmentName = recurseFragmentName;
        this.descendantLevel = descendantLevel;
        return this;
    }

    /**
     * Adds variable to graphql request.
     *
     * @param variable variable name
     * @param value    variable value
     * @return
     */
    public PCARequestBuilder withVariable(String variable, Object value) {
        variables.put(variable, value);
        return this;
    }

    /**
     * Adds context data to request
     *
     * @param data array of context data representations to add
     * @return
     */
    public PCARequestBuilder withContextData(ContextData... data) {
        for (ContextData newData : data) {
            this.contextData.addClaimValues(newData);
        }
        return this;
    }

    /**
     * Adds claim value to context data of request
     *
     * @param claim
     * @return
     */
    public PCARequestBuilder withClaim(ClaimValue claim) {
        this.contextData.addClaimValule(claim);
        return this;
    }

    /**
     * Adds operation name to request.
     *
     * @param operation operation name
     * @return
     */
    public PCARequestBuilder withOperation(String operation) {
        this.operationName = operation;
        return this;
    }

    /**
     * Specifies timeout in milliseconds to request.
     *
     * @param timeoutMillis timeout in milliseconds
     * @return
     */
    public PCARequestBuilder withTimeout(int timeoutMillis) {
        this.timeout = timeoutMillis;
        return this;
    }

    /**
     * Updates query with content include mode.
     *
     * @param contentIncludeMode
     * @return
     */
    public PCARequestBuilder withContentIncludeMode(ContentIncludeMode contentIncludeMode) {
        if (contentIncludeMode != null) {
            this.contentIncludeMode = contentIncludeMode;
        }
        return this;
    }

    /**
     * Includes/excludes region in query based on provided ContentIncludeMode.
     *
     * @param regionName
     * @param includeMode
     * @return
     */
    public PCARequestBuilder withIncludeRegion(String regionName, ContentIncludeMode includeMode) {
        return withIncludeRegion(regionName, includeMode != ContentIncludeMode.EXCLUDE);
    }

    /**
     * Includes/excludes region in query based on provided boolean value.
     *
     * @param regionName
     * @param isInclude
     * @return
     */
    public PCARequestBuilder withIncludeRegion(String regionName, boolean isInclude) {
        this.includeRegions.put(regionName, isInclude);
        return this;
    }

    /**
     * Updates variant args placeholder on query with url.
     *
     * @param url url
     * @return
     */
    public PCARequestBuilder withVariantArgs(String url) {
        this.variantArgs = url;
        return this;
    }

    /**
     * Updates query with custom meta filter.
     *
     * @param customMetaFilter custom meta filter
     * @return
     */
    public PCARequestBuilder withCustomMetaFilter(String customMetaFilter) {
        this.customMetaFilter = customMetaFilter;
        return this;
    }

    /**
     * Builds GraphQLRequest instance.
     *
     * @return
     */
    public GraphQLRequest build() {

        //load query
        query = queryHolder.getQuery(queryName);

        //load recursive
        query = expandRecursiveFragment(recurseFragmentName, descendantLevel);

        //inject fragments
        query = updateWithInjectFragments(injectFragments);

        //load all fragments
        query = updateQueryWithFragments(query);

        //include content?
        query = updateWithIncludeRegions(query, includeRegions);

        //inject variables
        query = QueryUtils.injectRenderContentArgs(query, this.contentIncludeMode == ContentIncludeMode.INCLUDE_AND_RENDER);
        query = QueryUtils.injectVariantsArgs(query, variantArgs);
        query = QueryUtils.injectCustomMetaFilter(query, customMetaFilter);

        this.variables.put("contextData", contextData.getClaimValues());
        return new GraphQLRequest(query, variables, operationName, timeout);
    }

    private String expandRecursiveFragment(String recurseFragmentName, int descendantLevel) {
        if (Strings.isNullOrEmpty(recurseFragmentName) || descendantLevel == 0) {
            return query;
        }
        String recurseFragment = queryHolder.getFragment(recurseFragmentName);
        return QueryUtils.expandRecursively(query, recurseFragment, descendantLevel);
    }

    private String updateWithInjectFragments(Set<String> injectFragments) {
        String fragmentList = injectFragments.stream()
                .map(fragment -> "..." + fragment + "\n")
                .reduce("", String::concat);

        return query.replace("@fragmentList", fragmentList);
    }

    private String replaceTag(String tagName, String value) {
        return query.replace("@" + tagName, value);
    }

    private String updateQueryWithFragments(String query) {
        Map<String, String> fragments = new HashMap<>();
        fragments = loadFragmentsRecursively(fragments, query);
        return fragments.values().stream().reduce(query, String::concat);
    }

    private Map<String, String> loadFragmentsRecursively(Map<String, String> loadedFragments, String queryPart) {
        Matcher matcher = FRAGMENT_NAMES_FROM_BODY.matcher(queryPart);
        while (matcher.find()) {
            String fragmentName = matcher.group("fragmentName");
            if (!loadedFragments.containsKey(fragmentName)) {
                String fragmentBody = queryHolder.getFragment(fragmentName);
                loadedFragments.put(fragmentName, fragmentBody);
                loadFragmentsRecursively(loadedFragments, fragmentBody);
            }
            String fragmentBody = queryHolder.getFragment(fragmentName);
            loadFragmentsRecursively(loadedFragments, fragmentBody);
        }
        return loadedFragments;
    }

    private String updateWithIncludeRegions(String query, Map<String, Boolean> includeRegions) {
        if (includeRegions.isEmpty()) {
            return query;
        }

        String result = query;
        for (Map.Entry<String, Boolean> entry : includeRegions.entrySet()) {
            result = QueryUtils.parseIncludeRegions(query, entry.getKey(), entry.getValue());
        }
        return result;
    }

}
