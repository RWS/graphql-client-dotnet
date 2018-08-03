package com.sdl.web.pca.client.request;

import java.io.IOException;
import java.io.InputStream;
import java.util.Dictionary;
import java.util.HashMap;

import org.apache.commons.io.IOUtils;

public class GraphQLRequest implements IGraphQLRequest {
    private String _operationName;
    private String _query;
    private HashMap<String, Object> _variables;
    private int _timeout;

    @Override
    public void setTimeout(int _timeout) {
        this._timeout = _timeout;
    }

    @Override
    public int getTimeout() {
        return _timeout;
    }

    @Override
    public void setOperationName(String operationName) {
        _operationName = operationName;
    }
    @Override
    public String getOperationName() {
        return _operationName;
    }
    @Override
    public void setQuery(String query) {
        _query = query;
    }
    @Override
    public String getQuery() {
        return _query;
    }
    @Override
    public void setVariables(HashMap<String, Object> variables) {
        _variables = variables;
    }
    @Override
    public HashMap<String, Object> getVariables() {
        return _variables;
    }

    public IGraphQLRequest AddVariable(String name, Object value)
    {
        _variables.put(name, value);
        return this;
    }
}
