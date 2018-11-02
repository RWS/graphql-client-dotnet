package com.sdl.web.pca.client.query;

import com.sdl.web.pca.client.exception.ApiClientException;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import static java.nio.charset.StandardCharsets.UTF_8;

/**
 * Holds queries and fragments.
 */
public class QueryHolder {
    private Map<String, String> queries = new HashMap<>();
    private Map<String, String> fragments = new HashMap<>();

    public String getQuery(String queryName) {
        return queries.computeIfAbsent(queryName,
                s -> loadQueryFromResourcefile("queries/" + s));
    }

    public String getFragment(String fragmentName) {
        return fragments.computeIfAbsent(fragmentName,
                s -> loadQueryFromResourcefile("queries/fragments/" + s));
    }

    private String loadQueryFromResourcefile(String fileName) throws ApiClientException {
        String path = fileName + ".graphql";
        try {
            return IOUtils.toString(this.getClass().getClassLoader().getResourceAsStream(path), UTF_8);
        } catch (IOException e) {
            throw new ApiClientException("Unable to read resource " + path, e);
        }
    }

    private QueryHolder() {
    }

    private static class QueryHolderInstance {
        private static QueryHolder instance = new QueryHolder();
    }

    public static QueryHolder getInstance() {
        return QueryHolderInstance.instance;
    }
}
