package com.sdl.web.pca.client;

import java.util.List;

public class GraphQLResponse{
    private Object Data;
    private List<GraphQLError> Errors;


    public void setData(Object data) {
        Data = data;
    }


    public void setErrors(List<GraphQLError> errors) {
        Errors = errors;
    }


    public Object getData() {
        return Data;
    }


    public List<GraphQLError> getErrors() {
        return Errors;
    }
}
