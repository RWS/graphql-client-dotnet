using System.Collections.Generic;
using Sdl.Web.HttpClient.Auth;
using Newtonsoft.Json;

namespace Sdl.Web.GraphQL.Request
{
    public class GraphQLRequest : IGraphQLRequest
    {
        [JsonIgnore]
        public IAuthentication Authenticaton { get; set; }
        public string OperationName { get; set; }
        public string Query { get; set; }
        public IDictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();

        public IGraphQLRequest AddVariable(string name, object value)
        {
            Variables.Add(name, value);
            return this;
        }
    }
}
