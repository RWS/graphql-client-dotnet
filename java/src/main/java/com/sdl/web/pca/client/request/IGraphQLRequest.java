package com.sdl.web.pca.client.request;

import java.util.Dictionary;
import java.util.HashMap;

public interface IGraphQLRequest {
    String Query = null;
    String OperationName = null;
    HashMap<String, Object> Variables = null;
    int timeout=0;

    /* IGraphQLRequest AddVariable(String name, Object value);*/
    /*SerializationBinder Binder;*/
    /* List<JsonConverter> Convertors;*/

    void setQuery(String query);
    String getQuery();

    void setTimeout(int timeout);
    int getTimeout();

    void setOperationName(String operationName);
    String getOperationName();

    void setVariables(HashMap<String, Object> variables);
    HashMap<String, Object> getVariables();
}
