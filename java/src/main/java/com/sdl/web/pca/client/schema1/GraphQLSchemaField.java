package com.sdl.web.pca.client.schema;

public class GraphQLSchemaField {
    public String Name;
    public String Description;
    public GraphQLSchemaTypeInfo Type;

    public void setName(String name) {
        Name = name;
    }

    public String getName() {
        return Name;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getDescription() {
        return Description;
    }

    public void setType(GraphQLSchemaTypeInfo type) {
        Type = type;
    }

    public GraphQLSchemaTypeInfo getType() {
        return Type;
    }
}
