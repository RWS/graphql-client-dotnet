package com.sdl.web.Request;

import com.sdl.web.Auth.IAuthentication;

import java.util.Dictionary;

public class GraphQLRequest implements IGraphQLRequest{
    public IAuthentication Authenticaton;
    public String OperationName;
    public String Query;
    public Dictionary<String, Object> Variables;
    /*public SerializationBinder Binder;*/
   /* public List<JsonConverter> Convertors;*/

    @Override
    public void setAuthenticaton(IAuthentication authenticaton) {
        Authenticaton = authenticaton;
    }
    @Override
    public IAuthentication getAuthenticaton() {
        return Authenticaton;
    }
    @Override
    public void setOperationName(String operationName) {
        OperationName = operationName;
    }
    @Override
    public String getOperationName() {
        return OperationName;
    }
    @Override
    public void setQuery(String query) {
        Query = query;
    }
    @Override
    public String getQuery() {
        return Query;
    }
    @Override
    public void setVariables(Dictionary<String, Object> variables) {
        Variables = variables;
    }
    @Override
    public Dictionary<String, Object> getVariables() {
        return Variables;
    }

    public IGraphQLRequest AddVariable(String name, Object value)
    {
        Variables.put(name, value);
        return this;
    }
}
