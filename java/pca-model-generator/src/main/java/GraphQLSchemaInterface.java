import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class GraphQLSchemaInterface{
    public String name;
    public String description;
    public String kind;
}
