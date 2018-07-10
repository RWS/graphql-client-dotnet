using System.Collections.Generic;

namespace Sdl.Web.GraphQLClient.Response
{
    public class GraphQLError
    {
        public string Message { get; set; }
        public List<object> Path { get; set; }
        public List<GraphQLErrorLocation> Locations { get; set; }
        public GraphQLExtensions Extensions { get; set; }
    }
}
