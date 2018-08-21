import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class GraphQLSchemaTypeInfo{
    public String name;
    public String kind;
    public GraphQLSchemaTypeInfo ofType;
}
