import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class GraphQLSchemaField{
    public String name;
    public String description;
    public GraphQLSchemaTypeInfo type;
}
