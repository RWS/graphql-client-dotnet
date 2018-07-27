package com.sdl.web.pca.client.request;

import java.util.Dictionary;

public interface IGraphQLRequest {
    String Query = null;
    String OperationName = null;
    Dictionary<String, Object> Variables = null;

    /* IGraphQLRequest AddVariable(String name, Object value);*/
    /*SerializationBinder Binder;*/
    /* List<JsonConverter> Convertors;*/

    void setQuery(String query);
    String getQuery();

    void setOperationName(String operationName);
    String getOperationName();

    void setVariables(Dictionary<String, Object> variables);
    Dictionary<String, Object> getVariables();
}
