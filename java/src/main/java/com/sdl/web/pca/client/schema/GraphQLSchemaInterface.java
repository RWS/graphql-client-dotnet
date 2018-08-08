package com.sdl.web.pca.client.schema;

public class GraphQLSchemaInterface {
    public String Name;
    public String Description;
    public String Kind;

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

    public void setKind(String kind) {
        Kind = kind;
    }

    public String getKind() {
        return Kind;
    }
}
