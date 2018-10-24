using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Web.HttpClient;
using Sdl.Web.HttpClient.Auth;
using Sdl.Web.HttpClient.Request;
using Newtonsoft.Json;
using Sdl.Web.GraphQLClient.Exceptions;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.GraphQLClient.Response;
using Sdl.Web.GraphQLClient.Schema;
using Sdl.Web.PublicContentApi;
using Sdl.Web.PublicContentApi.Core;

namespace Sdl.Web.GraphQLClient
{
    /// <summary>
    /// GraphQL Client
    /// </summary>
    public class GraphQLClient : IGraphQLClient
    {
        protected readonly IHttpClient _httpClient;
        public ILogger Logger { get; } = new NullLogger();

        public GraphQLClient(string endpoint, IAuthentication auth = null)
        {
            _httpClient = new HttpClient.HttpClient(endpoint, auth);
        }

        public GraphQLClient(Uri endpoint, IAuthentication auth = null)
        {
            _httpClient = new HttpClient.HttpClient(endpoint, auth);
        }

        public GraphQLClient(IHttpClient httpClient, IAuthentication auth = null)
        {
            _httpClient = httpClient;
        }

        public GraphQLClient(string endpoint, ILogger logger, IAuthentication auth = null) 
        {
            Logger = logger ?? new NullLogger();
            _httpClient = new HttpClient.HttpClient(endpoint, Logger, auth);
        }

        public GraphQLClient(Uri endpoint, ILogger logger, IAuthentication auth = null)
        {
            Logger = logger ?? new NullLogger();
            _httpClient = new HttpClient.HttpClient(endpoint, Logger, auth);
        }

        public GraphQLClient(IHttpClient httpClient, ILogger logger)
        {
            Logger = logger ?? new NullLogger();
            _httpClient = httpClient;
        }

        /// <summary>
        /// Throw exception on any GraphQL errors.
        /// </summary>
        public bool ThrowOnAnyError { get; set; } = true;

        /// <summary>
        /// Get/Sets the timeout (ms) for the requests.
        /// </summary>
        public int Timeout
        {
            get { return _httpClient.Timeout; }
            set { _httpClient.Timeout = value; }
        }

        /// <summary>
        /// HttpClient used for performing the actual request.
        /// </summary>
        public IHttpClient HttpClient => _httpClient;

        /// <summary>
        /// Execute a GraphQL request
        /// </summary>
        /// <param name="request">Fully built GraphQL request</param>
        /// <returns>GraphQL Response</returns>
        public IGraphQLResponse Execute(IGraphQLRequest graphQLrequest)
        {
            try
            {
                var response = _httpClient.Execute<GraphQLResponse>(CreateHttpRequest(graphQLrequest));
                var responseData = response.ResponseData;               
                if (responseData == null) throw new GraphQLClientException(response);
                responseData.Headers = response.Headers;
                HandleErrors(responseData);
                if (Logger.IsTracingEnabled) Logger.Trace($"GraphQL Respose: {responseData.Data}");
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

        /// <summary>
        /// Execute a GraphQL request
        /// </summary>
        /// <typeparam name="T">Target Type of response data</typeparam>
        /// <param name="request">GraphQL request</param>
        /// <returns>GraphQL Response</returns>
        public IGraphQLTypedResponse<T> Execute<T>(IGraphQLRequest graphQLrequest)
        {
            try
            {
                var response = _httpClient.Execute<GraphQLTypedResponse<T>>(CreateHttpRequest(graphQLrequest));
                var responseData = response.ResponseData;
                if (responseData == null) throw new GraphQLClientException(response);
                responseData.Headers = response.Headers;
                HandleErrors(responseData);
                if (Logger.IsTracingEnabled) Logger.Trace($"GraphQL Respose: {responseData.Data}");
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

        /// <summary>
        /// Execute a GraphQL request (async)
        /// </summary>
        /// <param name="request">Fully built GraphQL request</param>
        /// <returns>GraphQL Response</returns>
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
                HandleErrors(responseData);
                if (Logger.IsTracingEnabled) Logger.Trace($"GraphQL Respose: {responseData.Data}");
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

        /// <summary>
        /// Execute a GraphQL request (async)
        /// </summary>
        /// <typeparam name="T">Target Type of response data</typeparam>
        /// <param name="request">GraphQL request</param>
        /// <returns>GraphQL Response</returns>
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
                HandleErrors(responseData);
                if (responseData.Data == null) throw new GraphQLClientException(response);
                if (Logger.IsTracingEnabled) Logger.Trace($"GraphQL Respose: {responseData.Data}");
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

        /// <summary>
        /// Gets GraphQL Schema from the GraphQL service.
        /// </summary>
        public GraphQLSchema Schema
        {
            get
            {
                try
                {
                    return Execute(new GraphQLRequest
                    {
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

        /// <summary>
        /// Gets GraphQL Schema from the GraphQL service (async).
        /// </summary>
        public async Task<GraphQLSchema> SchemaAsync()
        {
            try
            {
                return await ExecuteAsync(new GraphQLRequest
                {
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

        /// <summary>
        /// Returns the last errors that occured during the GraphQL request
        /// </summary>
        public List<GraphQLError> LastErrors { get; protected set; }

        private IHttpClientRequest CreateHttpRequest(IGraphQLRequest graphQLrequest)
            =>
                new HttpClientRequest
                {
                    ContentType = "application/json",
                    Method = "POST",
                    Body = graphQLrequest.Serialize(),
                    Headers = graphQLrequest.Headers,
                    Binder = graphQLrequest.Binder,
                    Convertors = graphQLrequest.Convertors
                };

        private void HandleErrors(IGraphQLResponse response)
        {
            LastErrors = response.Errors;
            if (response.Errors == null || response.Errors.Count <= 0) return;
            Logger.Warn("Errors were found during the last GraphQL request.");
            if (ThrowOnAnyError) throw new GraphQLClientException(response);
        }
    }
}
