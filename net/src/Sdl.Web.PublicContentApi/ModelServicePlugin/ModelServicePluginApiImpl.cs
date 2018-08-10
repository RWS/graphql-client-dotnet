using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Sdl.Web.Core;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Exceptions;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    public class ModelServicePluginApiImpl : IModelServicePluginApi, IModelServicePluginApiAsync
    {
        private readonly IGraphQLClient _client;
        private readonly ILogger _logger;

        public ModelServicePluginApiImpl(IGraphQLClient client, ILogger logger)
        {
            _client = client;
            _logger = logger ?? new NullLogger();
        }

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData)
        {
            try
            {
                UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
                var response = _client.Execute(new GraphQLRequest
                {
                    Query = InjectRenderContentArgs(Queries.Load("PageModelById", true), renderContent),
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
            catch (RuntimeBinderException e)
            {
                throw new PcaException($"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, pageId:{pageId}", e);
            }
        }

        public dynamic GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData)
        {
            try
            {
                UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
                var response = _client.Execute(new GraphQLRequest
                {
                    Query = InjectRenderContentArgs(Queries.Load("PageModelByUrl", true), renderContent),
                    Variables = new Dictionary<string, object>
                    {
                        {"namespaceId", ns},
                        {"publicationId", publicationId},
                        {"url", url},
                        {"contextData", contextData.ClaimValues}
                    },
                    OperationName = "page"
                });
                return response.Data.page.rawContent.data;
            }
            catch (RuntimeBinderException e)
            {
                throw new PcaException($"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, url:{url}", e);
            }
        }

        public dynamic GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData)
        {
            try
            {
                UpdateContextData(ref contextData, contentType, modelType, dcpType);

                var response = _client.Execute(new GraphQLRequest
                {
                    Query = InjectRenderContentArgs(Queries.Load("EntityModelById", true), renderContent),
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
            catch (RuntimeBinderException e)
            {
                throw new PcaException(
                    $"Failed to get enity model data (namespaceId:{ns}, publicationId:{publicationId}, entityId:{entityId}",
                    e);
            }
        }

        public TaxonomySitemapItem GetSitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData)
        {
            try
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
                    Convertors = new List<JsonConverter> {new TaxonomyItemConvertor()}
                });
                return response.TypedResponseData.Sitemap;
            }
            catch (RuntimeBinderException e)
            {
                throw new PcaException(
                    $"Failed to get sitemap (namespaceId:{ns}, publicationId:{publicationId}, descendantLevels:{descendantLevels}",
                    e);
            }
        }

        public TaxonomySitemapItem GetSitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, bool includeAncestors,
            IContextData contextData)
        {
            try
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
                        {"includeAncestors", includeAncestors},
                        {"contextData", contextData.ClaimValues}
                    },
                    Convertors = new List<JsonConverter> {new TaxonomyItemConvertor()}
                });
                return response.TypedResponseData.SitemapSubtree;
            }
            catch (RuntimeBinderException e)
            {
                throw new PcaException(
                    $"Failed to get sitemap subtree (namespaceId:{ns}, publicationId:{publicationId}, taxonomyNodeId{taxonomyNodeId}, descendantLevels:{descendantLevels}",
                    e);
            }
        }

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, int pageId,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
                var response = await _client.ExecuteAsync(new GraphQLRequest
                {
                    Query = InjectRenderContentArgs(Queries.Load("PageModelById", true), renderContent),
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
            catch (RuntimeBinderException e)
            {
                throw new PcaException($"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, pageId:{pageId}", e);
            }
        }

        public async Task<dynamic> GetPageModelDataAsync(ContentNamespace ns, int publicationId, string url,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
                var response = await _client.ExecuteAsync(new GraphQLRequest
                {
                    Query = InjectRenderContentArgs(Queries.Load("PageModelByUrl", true), renderContent),
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
            catch (RuntimeBinderException e)
            {
                throw new PcaException($"Failed to get page model data (namespaceId:{ns}, publicationId:{publicationId}, url:{url}", e);
            }
        }

        public async Task<dynamic> GetEntityModelDataAsync(ContentNamespace ns, int publicationId, int entityId,
            ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                UpdateContextData(ref contextData, contentType, modelType, dcpType);

                var response = await _client.ExecuteAsync(new GraphQLRequest
                {
                    Query = InjectRenderContentArgs(Queries.Load("EntityModelById", true), renderContent),
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
            catch (RuntimeBinderException e)
            {
                throw new PcaException(
                   $"Failed to get enity model data (namespaceId:{ns}, publicationId:{publicationId}, entityId:{entityId}",
                   e);
            }
        }

        public async Task<TaxonomySitemapItem> GetSitemapAsync(ContentNamespace ns, int publicationId,
            int descendantLevels, IContextData contextData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
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
                    Convertors = new List<JsonConverter> {new TaxonomyItemConvertor()}
                }, cancellationToken);
                return response.Data.sitemap;
            }
            catch (RuntimeBinderException e)
            {
                throw new PcaException(
                   $"Failed to get sitemap (namespaceId:{ns}, publicationId:{publicationId}, descendantLevels:{descendantLevels}",
                   e);
            }
        }

        public async Task<TaxonomySitemapItem> GetSitemapSubtreeAsync(ContentNamespace ns, int publicationId,
            string taxonomyNodeId, int descendantLevels, bool includeAncestors,
            IContextData contextData, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
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
                        {"includeAncestors", includeAncestors},
                        {"contextData", contextData.ClaimValues}
                    },
                    Convertors = new List<JsonConverter> {new TaxonomyItemConvertor()}
                }, cancellationToken);
                return response.Data.sitemapSubtree;
            }
            catch (RuntimeBinderException e)
            {
                throw new PcaException(
                   $"Failed to get sitemap subtree (namespaceId:{ns}, publicationId:{publicationId}, taxonomyNodeId{taxonomyNodeId}, descendantLevels:{descendantLevels}",
                   e);
            }
        }

        protected ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ContentType,
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
            Uri = ModelServiceClaimUris.EntityDcpType,
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

        protected static string InjectRenderContentArgs(string query, bool renderContent)
            => query.Replace("@renderContentArgs", $"(renderContent: {(renderContent ? "true" : "false")})");
    }
}
