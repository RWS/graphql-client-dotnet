using System;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.HttpClient;
using Sdl.Web.HttpClient.Auth;
using Sdl.Web.HttpClient.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sdl.Web.GraphQLClient.Exceptions;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;

namespace Sdl.Web.GraphQLClient
{
    public class GraphQLClient : IGraphQLClient
    {
        protected readonly IHttpClient _httpClient;
        protected readonly IAuthentication _auth;

        public GraphQLClient(string endpoint, IAuthentication auth = null)
        {
            _httpClient = new HttpClient.HttpClient(endpoint);
            _auth = auth;
        }

        public GraphQLClient(Uri endpoint, IAuthentication auth = null)
        {
            _httpClient = new HttpClient.HttpClient(endpoint);
            _auth = auth;
        }

        public GraphQLClient(IHttpClient httpClient, IAuthentication auth = null)
        {
            _httpClient = httpClient;
            _auth = auth;
        }

        public IHttpClient HttpClient => _httpClient;

        public int Timeout
        {
            get { return _httpClient.Timeout; }
            set { _httpClient.Timeout = value; }
        }

        public IGraphQLResponse Execute(IGraphQLRequest graphQLrequest)
        {
            try
            {
                var response = _httpClient.Execute<GraphQLResponse>(CreateHttpRequest(graphQLrequest));
                var responseData = response.ResponseData;
                if (responseData == null) throw new GraphQLClientException(response);
                responseData.Headers = response.Headers;
                if (responseData.Errors != null && responseData.Errors.Count > 0)
                    throw new GraphQLClientException(response);
                return responseData;
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

        public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest graphQLrequest)
        {
            try
            {
                var response = _httpClient.Execute<GraphQLTypedResponse<T>>(CreateHttpRequest(graphQLrequest));
                var responseData = response.ResponseData;
                if (responseData == null) throw new GraphQLClientException(response);
                responseData.Headers = response.Headers;
                if (responseData.Errors != null && responseData.Errors.Count > 0)
                    throw new GraphQLClientException(response);
                if (responseData.Data == null) throw new GraphQLClientException(response);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                if (graphQLrequest.Convertors == null || graphQLrequest.Convertors.Count <= 0)
                    responseData.TypedResponseData = responseData.Data.ToObject<T>();
                else
                {
                    foreach (var x in graphQLrequest.Convertors)
                    {
                        settings.Converters.Add(x);
                    }
                    responseData.TypedResponseData = JsonConvert.DeserializeObject<T>(responseData.Data.ToString(),
                        settings);
                }
                return responseData;
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
                        _httpClient.ExecuteAsync<GraphQLResponse>(CreateHttpRequest(graphQLrequest), cancellationToken);
                var responseData = response.ResponseData;
                if (responseData == null) throw new GraphQLClientException(response);
                responseData.Headers = response.Headers;
                if (responseData.Errors != null && responseData.Errors.Count > 0)
                    throw new GraphQLClientException(response);
                return responseData;
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

        public async Task<IGraphQLTypedResponse<T>> ExecuteAsync<T>(IGraphQLRequest graphQLrequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response =
                    await
                        _httpClient.ExecuteAsync<GraphQLTypedResponse<T>>(CreateHttpRequest(graphQLrequest),
                            cancellationToken);
                var responseData = response.ResponseData;
                if (responseData == null) throw new GraphQLClientException(response);
                responseData.Headers = response.Headers;
                if (responseData.Errors != null && responseData.Errors.Count > 0)
                    throw new GraphQLClientException(response);
                if (responseData.Data == null) throw new GraphQLClientException(response);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                if (graphQLrequest.Convertors == null || graphQLrequest.Convertors.Count <= 0)
                    responseData.TypedResponseData = responseData.Data.ToObject<T>();
                else
                {
                    foreach (var x in graphQLrequest.Convertors)
                    {
                        settings.Converters.Add(x);
                    }
                    responseData.TypedResponseData = JsonConvert.DeserializeObject<T>(responseData.Data.ToString(),
                        settings);
                }
                return responseData;
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
                        Query = Queries.Load("IntrospectionQuery", false),
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
                    Query = Queries.Load("IntrospectionQuery", false),
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
                        }),
                    Authenticaton = graphQLrequest.Authenticaton ?? _auth,
                    Headers = graphQLrequest.Headers,
                    Binder = graphQLrequest.Binder,
                    Convertors = graphQLrequest.Convertors
                };
    }
}
