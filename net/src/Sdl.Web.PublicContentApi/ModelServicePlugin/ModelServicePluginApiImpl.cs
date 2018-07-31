using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    public class ModelServicePluginApiImpl : IModelServicePluginApi, IModelServicePluginApiAsync
    {
        private readonly IGraphQLClient _client;

        public ModelServicePluginApiImpl(IGraphQLClient client)
        {
            _client = client;
        }

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var response = _client.Execute(new GraphQLRequest
            {
                Query = Queries.Load("PageModelById", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"contextData", contextData.ClaimValues}
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
                Query = Queries.Load("PageModelByUrl", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData.ClaimValues}
                }
            });
            return response.Data.page.rawContent.data;
        }

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, dcpType);

            var response = _client.Execute(new GraphQLRequest
            {
                Query = Queries.Load("EntityModelById", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"entityId", entityId},
                    {"contextData", contextData.ClaimValues}
                }
            });
            return response.Data.entity.rawContent.data;
        }
       
        public TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels, IContextData contextData)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }
            string query = Queries.Load("Sitemap", true);
            QueryHelpers.ExpandRecursiveFragment(ref query, null, descendantLevels);
            var response = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData.ClaimValues}
                },
                Convertors = new List<JsonConverter> { new TaxonomyItemConvertor() }
            });
            return response.TypedResponseData.Sitemap;
        }

        public TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId, int descendantLevels,
            IContextData contextData)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }
            string query = Queries.Load("SitemapSubtree", true);
            QueryHelpers.ExpandRecursiveFragment(ref query, null, descendantLevels);
            var response = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"taxonomyNodeId", taxonomyNodeId},
                    {"contextData", contextData.ClaimValues}
                },
                Convertors = new List<JsonConverter> { new TaxonomyItemConvertor() }
            });
            return response.TypedResponseData.SitemapSubtree;
        }
       
        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var response = await _client.ExecuteAsync(new GraphQLRequest
            {
                Query = Queries.Load("PageModelById", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken);
            return response.Data.page.rawContent.data;
        }

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url, ContentType contentType,
          DataModelType modelType, PageInclusion pageInclusion, IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var response = await _client.ExecuteAsync(new GraphQLRequest
            {
                Query = Queries.Load("PageModelByUrl", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken);
            return response.Data.page.rawContent.data;
        }

        public async Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateContextData(ref contextData, contentType, modelType, dcpType);

            var response = await _client.ExecuteAsync(new GraphQLRequest
            {
                Query = Queries.Load("EntityModelById", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"entityId", entityId},
                    {"contextData", contextData.ClaimValues}
                }
            }, cancellationToken);
            return response.Data.entity.rawContent.data;
        }

        public async Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId, int descendantLevels, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }
            string query = Queries.Load("Sitemap", true);
            QueryHelpers.ExpandRecursiveFragment(ref query, null, descendantLevels);
            var response = await _client.ExecuteAsync(new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData.ClaimValues}
                },
                Convertors = new List<JsonConverter> { new TaxonomyItemConvertor() }
            }, cancellationToken);
            return response.Data.sitemap;
        }

        public async Task<TaxonomySitemapItem> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId, string taxonomyNodeId, int descendantLevels,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }
            string query = Queries.Load("SitemapSubtree", true);
            QueryHelpers.ExpandRecursiveFragment(ref query, null, descendantLevels);
            var response = await _client.ExecuteAsync(new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"taxonomyNodeId", taxonomyNodeId},
                    {"contextData", contextData.ClaimValues}
                },
                Convertors = new List<JsonConverter> { new TaxonomyItemConvertor() }
            }, cancellationToken);
            return response.Data.sitemapSubtree;
        }

        protected ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof(ContentType), contentType)
        };

        protected ClaimValue CreateClaim(DataModelType dataModelType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof(DataModelType), dataModelType)
        };

        protected ClaimValue CreateClaim(PageInclusion pageInclusion) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof(PageInclusion), pageInclusion)
        };

        protected ClaimValue CreateClaim(DcpType dcpType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof(DcpType), dcpType)
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
    }
}
