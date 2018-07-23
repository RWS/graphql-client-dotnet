package com.sdl.web.Request;

import com.sdl.web.Auth.IAuthentication;

import java.util.Dictionary;

public interface IGraphQLRequest {
    String Query = null;
    String OperationName = null;
    Dictionary<String, Object> Variables = null;
    IAuthentication Authenticaton = null;

    /* IGraphQLRequest AddVariable(String name, Object value);*/
    /*SerializationBinder Binder;*/
    /* List<JsonConverter> Convertors;*/

    void setQuery(String query);
    String getQuery();

    void setOperationName(String operationName);
    String getOperationName();

    void setVariables(Dictionary<String, Object> variables);
    Dictionary<String, Object> getVariables();

    void setAuthenticaton(IAuthentication authenticaton);
    IAuthentication getAuthenticaton();

}
