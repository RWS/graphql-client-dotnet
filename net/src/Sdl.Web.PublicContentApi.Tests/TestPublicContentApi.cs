using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.HttpClient;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.Tests
{
    [TestClass]
    public class TestPublicContentApi : TestClass
    {
        private class MockGraphQLClient : IGraphQLClient
        {
            private readonly Dictionary<string, object> _expectedVariables;

            public MockGraphQLClient(Dictionary<string, object> expectedVariables)
            {
                _expectedVariables = expectedVariables;
            }

            public int Timeout { get; set; }
            public IHttpClient HttpClient { get; }
            public IGraphQLResponse Execute(IGraphQLRequest request)
            {
                ValidateRequest(request);
                return new GraphQLResponse {Data = new object()};
            }

            public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest request)
            {
                ValidateRequest(request);
                return new GraphQLTypedResponse<T> {TypedResponseData =  (T)((object)( new ContentQuery()))};
            }

            public Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken)
            {
                ValidateRequest(request);
                return null;
            }

            public Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken)
            {
                ValidateRequest(request);
                return null;
            }

            public GraphQLSchema Schema { get; }
            public Task<GraphQLSchema> SchemaAsync()
            {
                throw new System.NotImplementedException();
            }

            private void ValidateRequest(IGraphQLRequest request)
            {
                Assert.AreEqual(request.Variables.Count, _expectedVariables.Count, "Variables are expected to be the same length.");

                foreach (var x in request.Variables)
                {
                    if (_expectedVariables.ContainsKey(x.Key))
                    {
                        if (x.Key == "contextData")
                        {
                            var a = request.Variables[x.Key] as List<ClaimValue>;
                            var b = _expectedVariables[x.Key] as List<ClaimValue>;
                            Assert.AreEqual(a.Count, b.Count, "ClaimValue count mismatch");
                            for (int i = 0; i < a.Count; i++)
                            {
                                Assert.AreEqual(a[i], b[i], "Claims dont match");
                            }
                        }
                        else
                        {
                            Assert.AreEqual(request.Variables[x.Key], _expectedVariables[x.Key],
                                $"Variable value mismatch {x.Key}");
                        }
                    }
                    else
                    {
                        Assert.Fail("Excepted variable not found");
                    }
                }
            }
        }

        [TestMethod]
        public void TestGlobalContextData()
        {
            var claims = new ContextData
            {
                ClaimValues = new List<ClaimValue>
                {
                    new ClaimValue
                    {
                        Uri = "x.y.z",
                        Type = ClaimValueType.STRING,
                        Value = "test"
                    }
                }
            };

            // these claims get added by default
            claims.ClaimValues.Add(GraphQLRequests.CreateClaim(DataModelType.R2));
            claims.ClaimValues.Add(GraphQLRequests.CreateClaim(ContentType.MODEL));
            claims.ClaimValues.Add(GraphQLRequests.CreateClaim(TcdlLinkRendering.Relative));
            claims.ClaimValues.Add(GraphQLRequests.CreateClaim(ModelServiceLinkRendering.Relative));

            var expected = new Dictionary<string, object>
            {
                {"namespaceId", ContentNamespace.Sites },
                {"publicationId", 1 },
                {"url", "/" },
                {"contextData", claims.ClaimValues}
            };

            var client = CreateClient(new MockGraphQLClient(expected));

            client.GetBinaryComponent(ContentNamespace.Sites, 1, "/", null, claims);

            client.GlobalContextData = claims;
            client.GetBinaryComponent(ContentNamespace.Sites, 1, "/", null, null);
        }
    }
}
