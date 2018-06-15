using System.Collections.Generic;
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
        IAuthentication Authenticaton { get; set; }
    }
}
