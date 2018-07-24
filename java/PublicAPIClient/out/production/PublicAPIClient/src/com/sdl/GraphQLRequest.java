package com.sdl;

import java.util.Dictionary;
import java.util.HashMap;

public class GraphQLRequest implements IGraphQLRequest {

    private String OperationName;
    private String Query;
    private HashMap<String, Object> Variables;

    @Override
    public String getOperationName() {
        return OperationName;
    }

    @Override
    public void setOperationName(String operationName) {
        OperationName = operationName;
    }

    @Override
    public String getQuery() {
        return Query;
    }

    @Override
    public void setQuery(String query) {
        Query = query;
    }

    @Override
    public HashMap<String, Object> getVariables() {
        return Variables;
    }

    @Override
    public void setVariables(HashMap<String, Object> variables) {
        Variables = variables;
    }

    public final IGraphQLRequest AddVariable(String name, Object value) {
        this.Variables.put(name, value);
        return this;
    }

}


