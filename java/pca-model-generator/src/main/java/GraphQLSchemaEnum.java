import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class GraphQLSchemaEnum{
    public String name;
    public String description;
}
