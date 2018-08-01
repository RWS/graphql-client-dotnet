using Sdl.Web.HttpClient;
using System.IO;
using System.Reflection;
using System.Text;
using Sdl.Web.GraphQLClient;

namespace Sdl.Web.PublicContentApi.Tests
{
    public abstract class TestClass
    {
        /// <summary>
        /// Create a mock client that will return a raw graphQL response json block
        /// </summary>
        /// <param name="jsonResponse">Json returned directly from graphQL client</param>
        /// <returns></returns>
        public static PublicContentApi CreateClient(string jsonResponse)
        {
            IHttpClient mockHttpClient = new MockHttpClient(
                Encoding.UTF8.GetBytes(jsonResponse),
                "application/json"
                );

            IGraphQLClient graphQL = new GraphQLClient.GraphQLClient(mockHttpClient);
            return new PublicContentApi(graphQL);
        }

        public static string LoadResource(string resourceName)
        {
            string resource = $"Sdl.Web.PublicContentApi.Tests.Resources.{resourceName}";
            using (Stream stm = Assembly.GetCallingAssembly().GetManifestResourceStream(resource))
            {
                if (stm != null) return new StreamReader(stm).ReadToEnd();
            }
            return string.Empty;
        }
    }
}
