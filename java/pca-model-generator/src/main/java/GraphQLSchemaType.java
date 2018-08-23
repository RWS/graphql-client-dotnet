import java.util.List;

public class    GraphQLSchemaType {
    public String kind;
    public String name;
    public String description;
    public List<GraphQLSchemaField> fields;
    public List<GraphQLSchemaEnum> enumValues;
    public List<GraphQLSchemaInterface> interfaces;
    public List<GraphQLSchemaTypeInfo> possibleTypes;
    public List<GraphQLSchemaField> inputFields;
}

