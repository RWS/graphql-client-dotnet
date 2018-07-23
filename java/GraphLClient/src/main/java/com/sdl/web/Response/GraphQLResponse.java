package com.sdl.web.Response;

import java.util.List;

public class GraphQLResponse implements IGraphQLResponse{
    private Object Data;
    private List<GraphQLError> Errors;

    @Override
    public void setData(Object data) {
        Data = data;
    }

    @Override
    public void setErrors(List<GraphQLError> errors) {
        Errors = errors;
    }

    @Override
    public Object getData() {
        return Data;
    }

    @Override
    public List<GraphQLError> getErrors() {
        return Errors;
    }
}
