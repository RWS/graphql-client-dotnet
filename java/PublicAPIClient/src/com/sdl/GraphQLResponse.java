package com.sdl;

import java.util.List;

public class GraphQLResponse implements IGraphQLResponse{

    private Object Data;
    private List<GraphQLError> Errors;

    @Override
    public Object getData() {
        return Data;
    }

    @Override
    public void setData(Object data) {
        Data = data;
    }

    @Override
    public List<GraphQLError> getErrors() {
        return Errors;
    }

    @Override
    public void setErrors(List<GraphQLError> errors) {
        Errors = errors;
    }
}
