package com.sdl.web.Schema;

import java.util.List;

public class GraphQLSchemaType {
    public String Kind;
    public String Name;
    public String Description;
    public List<GraphQLSchemaField> Fields;
    public List<GraphQLSchemaEnum> EnumValues;
    public List<GraphQLSchemaInterface> Interfaces;
    public List<GraphQLSchemaTypeInfo> PossibleTypes;
    public List<GraphQLSchemaField> InputFields;

    public void setKind(String kind) {
        Kind = kind;
    }

    public String getKind() {
        return Kind;
    }

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

    public void setFields(List<GraphQLSchemaField> fields) {
        Fields = fields;
    }

    public List<GraphQLSchemaField> getFields() {
        return Fields;
    }

    public void setEnumValues(List<GraphQLSchemaEnum> enumValues) {
        EnumValues = enumValues;
    }

    public List<GraphQLSchemaEnum> getEnumValues() {
        return EnumValues;
    }

    public void setInterfaces(List<GraphQLSchemaInterface> interfaces) {
        Interfaces = interfaces;
    }

    public List<GraphQLSchemaInterface> getInterfaces() {
        return Interfaces;
    }

    public void setPossibleTypes(List<GraphQLSchemaTypeInfo> possibleTypes) {
        PossibleTypes = possibleTypes;
    }

    public List<GraphQLSchemaTypeInfo> getPossibleTypes() {
        return PossibleTypes;
    }

    public void setInputFields(List<GraphQLSchemaField> inputFields) {
        InputFields = inputFields;
    }

    public List<GraphQLSchemaField> getInputFields() {
        return InputFields;
    }
}
