package com.sdl.web.pca.client.schema;

public class GraphQLSchemaTypeInfo {
    public String Name;
    public String Kind;
    public GraphQLSchemaTypeInfo OfType;

    public void setName(String name) {
        Name = name;
    }

    public String getName() {
        return Name;
    }

    public void setKind(String kind) {
        Kind = kind;
    }

    public String getKind() {
        return Kind;
    }

    public void setOfType(GraphQLSchemaTypeInfo ofType) {
        OfType = ofType;
    }

    public GraphQLSchemaTypeInfo getOfType() {
        return OfType;
    }
}
