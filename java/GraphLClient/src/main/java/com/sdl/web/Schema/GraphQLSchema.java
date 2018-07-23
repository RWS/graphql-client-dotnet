package com.sdl.web.Schema;

import java.util.List;

public class GraphQLSchema {
    private Object QueryType;
    private Object MutationType;
    private Object SubscriptionType;
    private List<GraphQLSchemaType> Types;
    private Object Directives;
    private Object Args;

    public void setQueryType(Object queryType) {
        QueryType = queryType;
    }

    public Object getQueryType() {
        return QueryType;
    }

    public void setMutationType(Object mutationType) {
        MutationType = mutationType;
    }

    public Object getMutationType() {
        return MutationType;
    }

    public void setSubscriptionType(Object subscriptionType) {
        SubscriptionType = subscriptionType;
    }

    public Object getSubscriptionType() {
        return SubscriptionType;
    }

    public void setTypes(List<GraphQLSchemaType> types) {
        Types = types;
    }

    public List<GraphQLSchemaType> getTypes() {
        return Types;
    }

    public void setDirectives(Object directives) {
        Directives = directives;
    }

    public Object getDirectives() {
        return Directives;
    }

    public void setArgs(Object args) {
        Args = args;
    }

    public Object getArgs() {
        return Args;
    }
}
