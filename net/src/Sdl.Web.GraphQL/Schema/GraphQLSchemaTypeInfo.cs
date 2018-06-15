namespace Sdl.Web.GraphQL.Schema
{
    public class GraphQLSchemaTypeInfo
    {
        public string Name { get; set; }
        public string Kind { get; set; }
        public GraphQLSchemaTypeInfo OfType { get; set; }
    }
}
