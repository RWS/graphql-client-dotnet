package com.sdl;

import java.util.List;

public interface IGraphQLResponse {

    Object getData();
    void setData(Object data);

    List<GraphQLError> getErrors();
    void setErrors(List<GraphQLError> errors);
}
