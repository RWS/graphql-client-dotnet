import java.util.List;

public class GraphQLSchema
{
    public GraphQLSchema(){
    }

    /*private Object data;

    public void setData(Object mydata){
        data=mydata;
    }

    public Object getData(){
        return data;
    }*/

    public GraphQLQueryType queryType;
    public Object mutationType;
    public Object subscriptionType;
    public List<GraphQLSchemaType> types;
    public Object directives;
    public Object args;

    /*public void setMutationType(Object mutationType){
     _MutationType = mutationType;
    }

    public Object getMutationType(){
        return _MutationType;
    }*/
}