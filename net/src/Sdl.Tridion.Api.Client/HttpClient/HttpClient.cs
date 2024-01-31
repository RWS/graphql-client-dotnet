using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sdl.Tridion.Api.Http.Client.Exceptions;
using Sdl.Tridion.Api.Http.Client.Request;
using Sdl.Tridion.Api.Http.Client.Response;
using Newtonsoft.Json;
using Sdl.Tridion.Api.Client.Core;
using Sdl.Tridion.Api.Http.Client.Auth;

namespace Sdl.Tridion.Api.Http.Client
{
    /// <summary>
    /// Http Client
    /// </summary>
    public class HttpClient : IHttpClient
    {
        public Uri BaseUri { get; set; }
        public int Timeout { get; set; } = 30000;
        public int RetryCount { get; set; } = 5;
        public string UserAgent { get; set; } = "SDL.PCA.NET";
        public HttpHeaders Headers { get; set; } = new HttpHeaders();
        public ILogger Logger { get; } = new NullLogger();
        protected readonly IAuthentication _auth;

        public HttpClient()
        { }

        public HttpClient(string endpoint)
        {
            BaseUri = new Uri(endpoint);
        }

        public HttpClient(string endpoint, IAuthentication auth)
        {
            BaseUri = new Uri(endpoint);
            _auth = auth;
        }

        public HttpClient(Uri endpoint)
        {
            BaseUri = endpoint;
        }

        public HttpClient(Uri endpoint, IAuthentication auth)
        {
            BaseUri = endpoint;
            _auth = auth;
        }

        public HttpClient(string endpoint, ILogger logger) : this(endpoint)
        {
            Logger = logger ?? new NullLogger();
        }

        public HttpClient(string endpoint, ILogger logger, IAuthentication auth) : this(endpoint, auth)
        {
            Logger = logger ?? new NullLogger();
        }

        public HttpClient(Uri endpoint, ILogger logger) : this(endpoint)
        {
            Logger = logger ?? new NullLogger();
        }

        public HttpClient(Uri endpoint, ILogger logger, IAuthentication auth) : this(endpoint, auth)
        {
            Logger = logger ?? new NullLogger();
        }

        public virtual IHttpClientResponse<T> Execute<T>(IHttpClientRequest clientRequest)
        {
            try
            {
                return RetryBlock<IHttpClientResponse<T>>(() =>
                {
                    var request = CreateHttpWebRequest(clientRequest);
                    using (var response = request.GetResponse())
                    {
                        var httpWebResponse = (HttpWebResponse) response;
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream == null) return default(HttpClientResponse<T>);
                            var data = ReadStream(responseStream);
                            LogErrorResponse(data);
                            var deserialized = Deserialize<T>(data, httpWebResponse.ContentType, clientRequest.Binder,
                                clientRequest.Convertors);
                            return new HttpClientResponse<T>
                            {
                                StatusCode = (int) httpWebResponse.StatusCode,
                                ContentType = httpWebResponse.ContentType,
                                Headers = new HttpHeaders(httpWebResponse.Headers),
                                ResponseData = deserialized
                            };
                        }
                    }
                }, RetryCount);
            }
            catch (WebException e)
            {
                if (e.Response == null) throw new HttpClientException(e.Message, e);
                var data = ReadStream(e.Response.GetResponseStream());
                throw new HttpClientException(
                    $"Failed to get http response from '{BaseUri}' with request: {clientRequest}",
                    e, (int)e.Status, Encoding.UTF8.GetString(data));
            }
            catch (Exception e)
            {
                throw new HttpClientException($"Failed to get http response from '{BaseUri}' with request: {clientRequest}", e);
            }
        }

        public virtual async Task<IHttpClientResponse<T>> ExecuteAsync<T>(IHttpClientRequest clientRequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                HttpWebRequest request = CreateHttpWebRequest(clientRequest);
                using (WebResponse response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)response;

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            byte[] data = await ReadStreamAsync(responseStream, cancellationToken).ConfigureAwait(false);

                            LogErrorResponse(data);

                            T deserialized =
                                await
                                    Task.Factory.StartNew(
                                        () =>
                                            Deserialize<T>(data, httpWebResponse.ContentType, clientRequest.Binder,
                                                clientRequest.Convertors), cancellationToken).ConfigureAwait(false);
                            return new HttpClientResponse<T>
                            {
                                StatusCode = (int)httpWebResponse.StatusCode,
                                ContentType = httpWebResponse.ContentType,
                                Headers = new HttpHeaders(httpWebResponse.Headers),
                                ResponseData = deserialized
                            };
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Logger.Error(e, $"Failed to get http response from '{BaseUri}' with request: '{clientRequest}'");
                byte[] data = ReadStream(e.Response.GetResponseStream());
                throw new HttpClientException($"Failed to get http response from '{BaseUri}' with request: {clientRequest}",
                    e, (int)e.Status, Encoding.UTF8.GetString(data));
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to get http response from '{BaseUri}' with request: '{clientRequest}'");
                throw new HttpClientException($"Failed to get http response from '{BaseUri}' with request: {clientRequest}", e);
            }
            Logger.Error($"Failed to get http response from '{BaseUri}' with request: '{clientRequest}'");
            throw new HttpClientException($"Failed to get http response from '{BaseUri}' with request: {clientRequest}");
        }

        protected virtual HttpWebRequest CreateHttpWebRequest(IHttpClientRequest clientRequest)
        {
            IHttpClientRequest requestCopy = new HttpClientRequest(clientRequest);
            requestCopy.Authenticaton = requestCopy.Authenticaton ?? _auth;
            Uri requestUri = requestCopy.BuildRequestUri(this);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = requestCopy.Method;
            request.Timeout = Timeout;
            request.ContentType = requestCopy.ContentType;
            request.UserAgent = UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            requestCopy.Authenticaton?.ApplyManualAuthentication(requestCopy);
            request.Credentials = requestCopy.Authenticaton;
            foreach (var x in Headers)
                request.Headers[x.Key] = x.Value?.ToString();
            foreach (var x in requestCopy.Headers)
                request.Headers[x.Key] = x.Value?.ToString();
            if (requestCopy.Method != "POST") return request;
            byte[] serialized = Serialize(requestCopy.Body, requestCopy.ContentType);
            using (Stream requestStream = request.GetRequestStream())
                requestStream.Write(serialized, 0, serialized.Length);
            if (!Logger.IsTracingEnabled) return request;
            Logger.Trace($"Performing Http Request:");
            Logger.Trace($"[{request.Method}] {request.RequestUri}");
            Logger.Trace($"[BODY] {requestCopy.Body}");
            foreach (var x in request.Headers.AllKeys)
            {
                Logger.Trace($"[HEADER] {x}={request.Headers[x]}");
            }
            return request;
        }

        private void LogErrorResponse(byte[] data)
        {
            string responseData = string.Empty;

            try
            {
                responseData = Encoding.UTF8.GetString(data);
            }
            catch { }

            if (Logger.IsTracingEnabled && responseData.Contains("errors")) //not the best way to do it, but couldn't see any other way
            {
                Logger.Trace($"Error Response: {responseData}");
            }
        }

        protected virtual byte[] ReadStream(Stream inputStream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream outputStream = new MemoryStream())
            {
                int read;
                while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    outputStream.Write(buffer, 0, read);
                return outputStream.ToArray();
            }
        }

        protected virtual async Task<byte[]> ReadStreamAsync(Stream inputStream, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var outputStream = new MemoryStream())
            {
                int read;
                while ((read = await inputStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                    outputStream.Write(buffer, 0, read);
                return outputStream.ToArray();
            }
        }

        protected virtual bool IsJsonMimeType(string contentType)
            => !string.IsNullOrEmpty(contentType) && contentType.ToLower().Contains("application/json");

        protected virtual byte[] Serialize(object data, string contentType)
        {
            if (data is byte[])
                return (byte[])data;

            if (data is string)
                return Encoding.UTF8.GetBytes((string)data);

            if (IsJsonMimeType(contentType))
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

            throw new Exception($"{contentType} not supported.");
        }

        protected virtual T Deserialize<T>(byte[] data, string contentType, SerializationBinder binder, List<JsonConverter> convertors)
        {
            if (data == null)
                return default(T);

            if (typeof(T) == typeof(byte[]))
                return (T)(object)data;

            if (typeof(T) == typeof(string))
                return (T)(object)Encoding.UTF8.GetString(data);

            if (!IsJsonMimeType(contentType))
                throw new HttpClientException($"ContentType: '{contentType}' not supported.");

            string json = Encoding.UTF8.GetString(data);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Binder = binder
            };
            if (convertors == null) return JsonConvert.DeserializeObject<T>(json, settings);
            foreach (var x in convertors)
                settings.Converters.Add(x);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        protected T RetryBlock<T>(Func<T> block, int retryCount)
        {
            if (retryCount < 0)
                return default(T);

            int sleepTime = 1000;
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    return block();
                }
                catch (Exception e)
                {
                    WebException webException = e as WebException;
                    if (webException != null)
                    {
                        Logger.Debug($"Received web exception status code = {webException.Status}");
                    }
                    if (retryCount <= 0)
                    {
                        Logger.Debug("Failed to receive a valid response after exhausting all retry attempts..");
                        if (webException == null) throw;
                        if (webException.Response == null) throw;
                        var responseStream = webException.Response.GetResponseStream();
                        if (responseStream == null) throw;
                        var resp = new StreamReader(responseStream).ReadToEnd();
                        dynamic obj = JsonConvert.DeserializeObject(resp);
                        var serverResponseMsg = obj.error.message;
                        Logger.Debug($"Response message from server was {serverResponseMsg}");
                        throw;
                    }

                    Logger.Debug($"Sleeping for {sleepTime}ms");
                    Thread.Sleep(sleepTime);
                    sleepTime += sleepTime;
                }
            }

            return default(T);
        }
    }
}
