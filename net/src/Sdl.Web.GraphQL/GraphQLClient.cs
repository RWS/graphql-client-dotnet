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
                var response = _httpClient.Execute<GraphQLResponse>(CreateHttpRequest(graphQLrequest)).ResponseData;
                if (response?.Errors == null || response.Errors.Count <= 0) return response;
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

        public T Execute<T>(IGraphQLRequest graphQLrequest)
        {
            try
            {
                var response = _httpClient.Execute<GraphQLResponse>(CreateHttpRequest(graphQLrequest)).ResponseData;
                if (response?.Errors == null || response.Errors.Count <= 0)
                {
                    if (response.Data == null) throw new GraphQLClientException(response);
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    if (graphQLrequest.Convertors == null || graphQLrequest.Convertors.Count <= 0)
                        return response.Data.ToObject<T>();
                    foreach (var x in graphQLrequest.Convertors)
                    {
                        settings.Converters.Add(x);
                    }
                    return JsonConvert.DeserializeObject<T>(response.Data.ToString(), settings);
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
                var response = await _httpClient.ExecuteAsync<GraphQLResponse>(
                    CreateHttpRequest(graphQLrequest),
                    cancellationToken);
                if (response.ResponseData?.Errors == null || response.ResponseData.Errors.Count <= 0)
                {
                    return response.ResponseData;
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

        public async Task<T> ExecuteAsync<T>(IGraphQLRequest graphQLrequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await _httpClient.ExecuteAsync<GraphQLResponse>(
                    CreateHttpRequest(graphQLrequest), cancellationToken);
                if (response.ResponseData?.Errors == null || response.ResponseData.Errors.Count <= 0)
                {
                    if (response.ResponseData.Data == null) throw new GraphQLClientException(response.ResponseData);
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    if (graphQLrequest.Convertors == null || graphQLrequest.Convertors.Count <= 0)
                        return response.ResponseData.Data.ToObject<T>();
                    foreach (var x in graphQLrequest.Convertors)
                    {
                        settings.Converters.Add(x);
                    }
                    return JsonConvert.DeserializeObject<T>(response.ResponseData.Data.ToString(), settings);
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
                        Query = Queries.Load("IntrospectionQuery"),
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
                    Query = Queries.Load("IntrospectionQuery"),
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
#if DEBUG
                    Body = JsonConvert.SerializeObject(graphQLrequest,
                        Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }),
#else
                    Body = JsonConvert.SerializeObject(graphQLrequest,
                        Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }).Replace("\\t", "").Replace("\\n", "").Replace("\\r", ""),
#endif
                    Authenticaton = graphQLrequest.Authenticaton,
                    Path = "/udp/content",
                    Binder = graphQLrequest.Binder,
                    Convertors = graphQLrequest.Convertors
                };
    }
}
