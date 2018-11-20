using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sdl.Tridion.Api.GraphQL.Client.Request
{
    /// <summary>
    /// Provide Batch GraphQL requests to let you build multiple GraphQL requests and
    /// execute them in a single http request.
    /// </summary>
    public class BatchGraphQLRequest : GraphQLRequest
    {
        public List<IGraphQLRequest> Requests { get; set; } = new List<IGraphQLRequest>();

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(Requests,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
