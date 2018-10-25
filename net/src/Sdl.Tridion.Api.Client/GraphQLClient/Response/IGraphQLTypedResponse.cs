namespace Sdl.Tridion.Api.GraphQL.Client.Response
{
    /// <summary>
    /// Typed GraphQL response for deserialization of results to strongly typed content model.
    /// </summary>
    /// <typeparam name="T">Type to deserialize results</typeparam>
    public interface IGraphQLTypedResponse<T> : IGraphQLResponse
    {
        /// <summary>
        /// Get typed response data from GraphQL request.
        /// </summary>
        T TypedResponseData { get; set; }
    }
}
