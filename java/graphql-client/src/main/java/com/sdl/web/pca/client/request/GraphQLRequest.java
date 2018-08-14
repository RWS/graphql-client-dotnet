package com.sdl.web.pca.client.request;

import java.util.HashMap;
import java.util.Objects;

public class GraphQLRequest {
    private String operationName;
    private String query;
    private HashMap<String, Object> variables;
    private int timeout;

    public String getOperationName() {
        return operationName;
    }

    public void setOperationName(String operationName) {
        this.operationName = operationName;
    }

    public String getQuery() {
        return query;
    }

    public void setQuery(String query) {
        this.query = query;
    }

    public HashMap<String, Object> getVariables() {
        return variables;
    }

    public void setVariables(HashMap<String, Object> variables) {
        this.variables = variables;
    }

    public int getTimeout() {
        return timeout;
    }

    public void setTimeout(int timeout) {
        this.timeout = timeout;
    }

    public GraphQLRequest addVariable(String name, String value) {
        variables.put(name, value);
        return this;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        GraphQLRequest that = (GraphQLRequest) o;
        return timeout == that.timeout &&
                Objects.equals(operationName, that.operationName) &&
                Objects.equals(query, that.query) &&
                Objects.equals(variables, that.variables);
    }

    @Override
    public int hashCode() {
        return Objects.hash(operationName, query, variables, timeout);
    }

    @Override
    public String toString() {
        return "GraphQLRequest{" +
                "operationName='" + operationName + '\'' +
                ", query='" + query + '\'' +
                ", variables=" + variables +
                ", timeout=" + timeout +
                '}';
    }
}
