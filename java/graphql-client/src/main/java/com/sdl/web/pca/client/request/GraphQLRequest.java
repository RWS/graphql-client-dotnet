package com.sdl.web.pca.client.request;

import java.util.HashMap;
import java.util.Objects;

public final class GraphQLRequest {
    private final String query;
    private final HashMap<String, Object> variables;

    /**
     * request timeout in milliseconds.
     */
    private final int timeout;

    public GraphQLRequest(String query, HashMap<String, Object> variables) {
        this(query, variables, 0);
    }

    public GraphQLRequest(String query, HashMap<String, Object> variables, int timeout) {
        this.query = query;
        this.variables = variables;
        this.timeout = timeout;
    }

    public String getQuery() {
        return query;
    }

    public HashMap<String, Object> getVariables() {
        return variables;
    }

    public int getTimeout() {
        return timeout;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        GraphQLRequest that = (GraphQLRequest) o;
        return timeout == that.timeout &&
                Objects.equals(query, that.query) &&
                Objects.equals(variables, that.variables);
    }

    @Override
    public int hashCode() {
        return Objects.hash(query, variables, timeout);
    }

    @Override
    public String toString() {
        return "GraphQLRequest{" +
                "query='" + query + '\'' +
                ", variables=" + variables +
                ", timeout=" + timeout +
                '}';
    }
}
