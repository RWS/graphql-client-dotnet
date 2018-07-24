package com.sdl;

import java.util.HashMap;

public interface IGraphQLRequest {

     String getQuery();
     void setQuery(String query);

     String getOperationName();
     void setOperationName(String operationName);

     HashMap<String, Object> getVariables();
     void setVariables(HashMap<String, Object> variables);
}
