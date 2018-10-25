using Sdl.Tridion.Api.Http.Client;
using System.IO;
using System.Reflection;
using System.Text;
using Sdl.Tridion.Api.GraphQL.Client;

namespace Sdl.Tridion.Api.Client.Tests
{
    public abstract class TestClass
    {
        /// <summary>
        /// Create a mock client that will return a raw graphQL response json block
        /// </summary>
        /// <param name="jsonResponse">Json returned directly from graphQL client</param>
        /// <returns></returns>
        public static ApiClient CreateClient(string jsonResponse)
        {
            IHttpClient mockHttpClient = new MockHttpClient(
                Encoding.UTF8.GetBytes(jsonResponse),
                "application/json"
                );

            IGraphQLClient graphQL = new GraphQLClient(mockHttpClient);
            return new ApiClient(graphQL);
        }

        public static ApiClient CreateClient(IGraphQLClient graphQL) 
            => new ApiClient(graphQL);

        public static string LoadResource(string resourceName)
        {
            string resource = $"Sdl.Tridion.Api.Client.Tests.Resources.{resourceName}";
            using (Stream stm = Assembly.GetCallingAssembly().GetManifestResourceStream(resource))
            {
                if (stm != null) return new StreamReader(stm).ReadToEnd();
            }
            return string.Empty;
        }
    }
}
