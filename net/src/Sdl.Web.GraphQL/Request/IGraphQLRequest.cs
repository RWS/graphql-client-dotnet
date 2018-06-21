﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Sdl.Web.HttpClient.Auth;

namespace Sdl.Web.GraphQL.Request
{
    /// <summary>
    /// GraphQL Request
    /// </summary>
    public interface IGraphQLRequest
    {
        /// <summary>
        /// GraphQL Query
        /// </summary>
        string Query { get; set; }

        /// <summary>
        /// GraphQL Operation Name
        /// </summary>
        string OperationName { get; set; }

        /// <summary>
        /// GraphQL Variables
        /// </summary>
        IDictionary<string, object> Variables { get; set; }

        /// <summary>
        /// Authentication used for request
        /// </summary>
        [JsonIgnore]
        IAuthentication Authenticaton { get; set; }

        /// <summary>
        /// Add Variable
        /// </summary>
        /// <param name="name">Variable name</param>
        /// <param name="value">Variable value</param>
        /// <returns>The request</returns>
        IGraphQLRequest AddVariable(string name, object value);

        /// <summary>
        /// Serialization binder used when deserializing the request.
        /// </summary>
        [JsonIgnore]
        SerializationBinder Binder { get; set; }

        /// <summary>
        /// Convertor used when deserialzing the request
        /// </summary>
        [JsonIgnore]
        List<JsonConverter> Convertors { get; set; }
    }
}
