package com.sdl.web.Response;


import java.util.List;

public interface IGraphQLResponse {
    Object Data = null;
    List<GraphQLError> Errors = null;

    void setData(Object data);
    Object getData();

    void setErrors(List<GraphQLError> errors);
    List<GraphQLError> getErrors();
}
