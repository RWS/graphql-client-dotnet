using System.Collections.Generic;

namespace Sdl.Web.GraphQL.Schema
{
    public class GraphQLSchemaType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GraphQLSchemaField> Fields { get; set; }
        public List<GraphQLSchemaEnum> EnumValues { get; set; }
        public List<GraphQLSchemaInterface> Interfaces { get; set; }
        public List<GraphQLSchemaTypeInfo> PossibleTypes { get; set; }
        public List<GraphQLSchemaField> InputFields { get; set; }
    }
}
