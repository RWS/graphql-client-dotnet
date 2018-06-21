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
    public class PublicContentApi : IGraphQLClient, IPublicContentApi, IPublicContentApiAsync, IModelServicePluginApi,
        IModelServicePluginApiAsync
    {
        private readonly IGraphQLClient _client;
        public PublicContentApi(IGraphQLClient graphQLclient)
        {
            _client = graphQLclient;
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

        #region IPublicContentApiAsync
            // todo
        #endregion

        #region IModelServicePluginApi

        protected ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (ContentType), contentType)
        };

        protected ClaimValue CreateClaim(DataModelType dataModelType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DataModelType), dataModelType)
        };

        protected ClaimValue CreateClaim(PageInclusion pageInclusion) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (PageInclusion), pageInclusion)
        };

        protected ClaimValue CreateClaim(DcpType dcpType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DcpType), dcpType)
        };

        protected void UpdateContextData(ref IContextData contextData, ContentType contentType,
            DataModelType dataModelType, PageInclusion pageInclusion)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }

            contextData.ClaimValues.Add(CreateClaim(contentType));
            contextData.ClaimValues.Add(CreateClaim(dataModelType));
            contextData.ClaimValues.Add(CreateClaim(pageInclusion));
        }

        protected void UpdateContextData(ref IContextData contextData, ContentType contentType,
            DataModelType dataModelType, DcpType dcpType)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }

            contextData.ClaimValues.Add(CreateClaim(contentType));
            contextData.ClaimValues.Add(CreateClaim(dataModelType));
            contextData.ClaimValues.Add(CreateClaim(dcpType));
        }

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var response = _client.Execute(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetPageModelDataByPageId,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"contextData", contextData}
                }
            });
            return response.Data.page.rawContent.data;
        }

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var response = _client.Execute(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetPageModelDataByUrl,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData}
                }
            });
            return response.Data.data;
        }

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, dcpType);

            var response = _client.Execute(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetEntityModelData,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"entityId", entityId},
                    {"contextData", contextData}
                }
            });
            return response.Data.rawContent.data;
        }

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetSitemap,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors,
            IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetSitemapSubtree,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"taxonomyNodeId", taxonomyNodeId},
                    {"includeAncestors", includeAncestors},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        #endregion

        #region IModelServicePluginApiAsync   

        public Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetSitemap(ContentNamespace ns, int publicationId, IContextData contextData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetSitemapAsync(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors,
            IContextData contextData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
