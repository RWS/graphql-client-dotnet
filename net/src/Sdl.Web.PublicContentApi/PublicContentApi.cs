using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.GraphQL;
using Sdl.Web.GraphQL.Request;
using Sdl.Web.GraphQL.Response;
using Sdl.Web.GraphQL.Schema;
using System.Threading;
using Newtonsoft.Json;
using Sdl.Web.PublicContentApi.ModelServicePlugin;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public class PublicContentApi : IGraphQLClient, IPublicContentApi, IPublicContentApiAsync, IModelServicePluginApi
    {
        private readonly IGraphQLClient _client;
        private readonly IModelServicePluginApi _modelserviceApi;
        public PublicContentApi(IGraphQLClient graphQLclient)
        {
            _client = graphQLclient;
            _modelserviceApi = new ModelServicePluginApiImpl(_client);
        }

        #region IGraphQLClient

        public GraphQLSchema Schema => _client.Schema;

        public Task<GraphQLSchema> SchemaAsync() => _client.SchemaAsync();

        public int Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        public IGraphQLResponse Execute(IGraphQLRequest request)
            => _client.Execute(request);

        public T Execute<T>(IGraphQLRequest request)
            => _client.Execute<T>(request);

        public Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken)
            => _client.ExecuteAsync(request, cancellationToken);

        public Task<T> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken)
            => _client.ExecuteAsync<T>(request, cancellationToken);

        #endregion

        #region IPublicContentApi

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetBinaryComponentById,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"contextData", contextData}
                }
            }).BinaryComponent;
        }

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url,
            IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetBinaryComponentByUrl,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData}
                }
            }).BinaryComponent;
        }

        private static string AddCustomMetaField(string query, string customMetaFilter)
        {
            return string.Format(query, customMetaFilter != null
                ? string.Format(Queries.CustomMetaField, $"\"{customMetaFilter}\"")
                : "");
        }
     
        public ItemConnection ExecuteItemQuery(InputItemFilter filter, IPagination pagination, 
            List<InputClaimValue> contextData, string customMetaFilter = null)
        {
            if (contextData == null)
                contextData = new List<InputClaimValue>();
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = AddCustomMetaField(Queries.ItemsQuery, customMetaFilter),
                Variables = new Dictionary<string, object>
                {
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"filter", filter},
                    {"contextData", contextData}
                },
                Convertors = new List<JsonConverter> { new ItemConvertor() }
            });
            return contenQuery.Items;
        }

        public Publication GetPublication(ContentNamespace ns, int publicationId,
            List<InputClaimValue> contextData, string customMetaFilter)
        {
            if (contextData == null)
                contextData = new List<InputClaimValue>();

            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = AddCustomMetaField(Queries.GetPublication, customMetaFilter),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData}
                }
            });
            return contenQuery.Publication;
        }

        public object GetPublicationMapping(ContentNamespace ns, string uri, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        #endregion

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData) 
            => _modelserviceApi.GetPageModelData(ns, publicationId, url, contentType, modelType, pageInclusion, contextData);

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData)
            =>
                _modelserviceApi.GetPageModelData(ns, publicationId, pageId, contentType, modelType, pageInclusion,
                    contextData);

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData)
            =>
                _modelserviceApi.GetEntityModelData(ns, publicationId, entityId, contentType, modelType, dcpType,
                    contextData);

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, IContextData contextData)
            => _modelserviceApi.GetSitemap(ns, publicationId, contextData);

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors,
            IContextData contextData)
            => _modelserviceApi.GetSitemap(ns, publicationId, taxonomyNodeId, includeAncestors, contextData);
    }
}
