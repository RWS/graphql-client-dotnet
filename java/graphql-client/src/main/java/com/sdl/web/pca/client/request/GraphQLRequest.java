package com.sdl.web.pca.client.request;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonInclude;

import java.util.Map;
import java.util.Objects;

import static com.fasterxml.jackson.annotation.JsonInclude.Include.NON_NULL;

@JsonInclude(NON_NULL)
public final class GraphQLRequest {
    private final String query;
    private final Map<String, Object> variables;
    private final String operationName;

    /**
     * request timeout in milliseconds.
     */
    @JsonIgnore
    private final int timeout;

    public GraphQLRequest(String query, Map<String, Object> variables) {
        this(query, variables, 0);
    }

    public GraphQLRequest(String query, Map<String, Object> variables, int timeout) {
        this(query, variables, null, 0);
    }


    public GraphQLRequest(String query, Map<String, Object> variables, String operationName, int timeout) {
        this.query = query;
        this.variables = variables;
        this.operationName = operationName;
        this.timeout = timeout;
    }

    public String getQuery() {
        return query;
    }

    public Map<String, Object> getVariables() {
        return variables;
    }

    public String getOperationName() {
        return operationName;
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
                Objects.equals(variables, that.variables) &&
                Objects.equals(operationName, that.operationName);
    }

    @Override
    public int hashCode() {
        return Objects.hash(query, variables, operationName, timeout);
    }

    @Override
    public String toString() {
        return "GraphQLRequest{" +
                "query='" + query + '\'' +
                ", variables=" + variables +
                ", operationName='" + operationName + '\'' +
                ", timeout=" + timeout +
                '}';
    }
}
