using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sdl.Web.GraphQLClient.Request
{
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
