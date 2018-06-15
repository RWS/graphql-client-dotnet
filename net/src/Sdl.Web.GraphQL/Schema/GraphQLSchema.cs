using System.Collections.Generic;

namespace Sdl.Web.GraphQL.Schema
{
    public class GraphQLSchema
    {
        public object QueryType { get; set; }
        public object MutationType { get; set; }
        public object SubscriptionType { get; set; }
        public List<GraphQLSchemaType> Types { get; set; }
        public object Directives { get; set; }
        public object Args { get; set; }
    }
}
