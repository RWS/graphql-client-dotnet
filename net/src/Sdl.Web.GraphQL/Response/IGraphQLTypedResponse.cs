namespace Sdl.Web.GraphQLClient.Response
{
    public interface IGraphQLTypedResponse<T> : IGraphQLResponse
    {
        T TypedResponseData { get; set; }
    }
}
