using System;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.GraphQL.Exceptions;
using Sdl.Web.GraphQL.Request;
using Sdl.Web.GraphQL.Response;
using Sdl.Web.GraphQL.Schema;
using Sdl.Web.HttpClient;
using Sdl.Web.HttpClient.Auth;
using Sdl.Web.HttpClient.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sdl.Web.GraphQL;
using Sdl.Web.GraphQL.Queries;

namespace DxaContentApiClient.GraphQL
{
    public class GraphQLClient : IGraphQLClient
    {
        protected readonly IHttpClient _httpClient;
        protected readonly IAuthentication _auth;

        public GraphQLClient(string endpoint, IAuthentication auth = null)
        {
            _httpClient = new HttpClient(endpoint);
            _auth = auth;
        }

        public GraphQLClient(Uri endpoint, IAuthentication auth = null)
        {
            _httpClient = new HttpClient(endpoint);
            _auth = auth;
        }

        public int Timeout
        {
            get { return _httpClient.Timeout; }
            set { _httpClient.Timeout = value; }
        }

        public IGraphQLResponse Execute(IGraphQLRequest graphQLrequest)
        {
            try
            {
                return _httpClient.Execute<GraphQLResponse>(CreateHttpRequest(graphQLrequest)).ResponseData;
            }
            catch (GraphQLClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new GraphQLClientException(e.Message, e);
            }
        }

        public T Execute<T>(IGraphQLRequest graphQLrequest)
        {
            try
            {
                var response = _httpClient.Execute<GraphQLResponse>(CreateHttpRequest(graphQLrequest)).ResponseData;
                if (response.Data != null)
                {
                    return response.Data.ToObject<T>();
                }
                throw new GraphQLClientException(response);
            }
            catch (GraphQLClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new GraphQLClientException(e.Message, e);
            }
        }

        public async Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest graphQLrequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _httpClient.ExecuteAsync<IGraphQLResponse>(CreateHttpRequest(graphQLrequest), cancellationToken);
                return response.ResponseData;
            }
            catch (GraphQLClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new GraphQLClientException(e.Message, e);
            }
        }

        public async Task<T> ExecuteAsync<T>(IGraphQLRequest graphQLrequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _httpClient.ExecuteAsync<IGraphQLResponse>(CreateHttpRequest(graphQLrequest), cancellationToken);
                if (response.ResponseData != null && response.ResponseData.Data != null)
                {
                    return response.ResponseData.Data.ToObject<T>();
                }

                throw new GraphQLClientException(response.ResponseData);
            }
            catch (GraphQLClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new GraphQLClientException(e.Message, e);
            }
        }

        public GraphQLSchema Schema
        {
            get
            {
                try
                {
                    return Execute(new GraphQLRequest
                    {
                        Authenticaton = _auth,
                        Query = Queries.IntrospectionQuery,
                        OperationName = "IntrospectionQuery"
                    }).Data.__schema.ToObject<GraphQLSchema>();
                }
                catch (GraphQLClientException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new GraphQLClientException(e.Message, e);
                }
            }
        }

        public async Task<GraphQLSchema> SchemaAsync()
        {
            try
            {
                return await ExecuteAsync(new GraphQLRequest
                {
                    Authenticaton = _auth,
                    Query = Queries.IntrospectionQuery,
                    OperationName = "IntrospectionQuery"
                }).Result.Data.__schema.ToObject<GraphQLSchema>();
            }
            catch (GraphQLClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new GraphQLClientException(e.Message, e);
            }
        }

        private IHttpClientRequest CreateHttpRequest(IGraphQLRequest graphQLrequest)
            =>
                new HttpClientRequest
                {
                    ContentType = "application/json",
                    Method = "POST",
                    Body = JsonConvert.SerializeObject(graphQLrequest,
                        Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }).Replace("\\t", "").Replace("\\n", "").Replace("\\r", ""),
                    Authenticaton = graphQLrequest.Authenticaton,
                    Path = "/udp/content"
                };
    }
}
