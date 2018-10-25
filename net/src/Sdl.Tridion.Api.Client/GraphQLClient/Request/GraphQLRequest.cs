using System.Collections.Generic;
using System.Runtime.Serialization;
using Sdl.Tridion.Api.Http.Client.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sdl.Tridion.Api.Http.Client;

namespace Sdl.Tridion.Api.GraphQL.Client.Request
{
    /// <summary>
    /// Represents a GraphQL request.
    /// </summary>
    public class GraphQLRequest : IGraphQLRequest
    {
        /// <summary>
        /// Get/Set Authentication used for this request.
        /// </summary>
        [JsonIgnore]
        public IAuthentication Authenticaton { get; set; }

        /// <summary>
        /// GraphQL Operation Name.
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// GraphQL Query in Json format.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// GraphQL Variables.
        /// </summary>
        public IDictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Http Headers to pass along during request.
        /// </summary>
        public HttpHeaders Headers { get; set; }

        /// <summary>
        /// Serialization Binder used during deserialization of result.
        /// </summary>
        public SerializationBinder Binder { get; set; }

        /// <summary>
        /// Json Serialization Convertors used during deserialization of result.
        /// </summary>
        public List<JsonConverter> Convertors { get; set; } = new List<JsonConverter>();

        /// <summary>
        /// Add Variable to request.
        /// </summary>
        /// <param name="name">Name of variable.</param>
        /// <param name="value">Value of variable.</param>
        /// <returns></returns>
        public IGraphQLRequest AddVariable(string name, object value)
        {
            Variables.Add(name, value);
            return this;
        }

        /// <summary>
        /// Serialize the request for http transport.
        /// </summary>
        /// <returns>Serialized request.</returns>
        public virtual string Serialize()
        {
            // Serialize ourselves and let the Json* attributes filter out
            // what is not required.
            // GraphQL serialization is very simple:
            //   {
            //     "query": "<query string>",
            //     "variables": "<query variables"
            //   }
            return JsonConvert.SerializeObject(this,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
