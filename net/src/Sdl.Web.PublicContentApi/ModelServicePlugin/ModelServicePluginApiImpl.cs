using System;
using System.Collections.Generic;
using Sdl.Web.GraphQL;
using Sdl.Web.GraphQL.Request;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    public class ModelServicePluginApiImpl : IModelServicePluginApi
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
                Query = Queries.Load("PageModelById"),
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
                Query = Queries.Load("PageModelByUrl"),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData}
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
                Query = Queries.Load("EntityModelById"),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"entityId", entityId},
                    {"contextData", contextData}
                }
            });
            return response.Data.entity.rawContent.data;
        }

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, IContextData contextData)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }
            var response = _client.Execute(new GraphQLRequest
            {
                Query = Queries.Load("Sitemap") + Queries.Load("SitemapFragments"),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData}
                }
            });
            return response.Data.sitemap;
        }

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors, int descendantLevels,
            IContextData contextData)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }
            var response = _client.Execute(new GraphQLRequest
            {
                Query = Queries.Load("SitemapSubtree") + Queries.Load("SitemapFragments"),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"taxonomyNodeId", taxonomyNodeId},
                    {"contextData", contextData}
                }
            });
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

        public dynamic GetSitemap(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors, IContextData contextData)
        {
            throw new NotImplementedException();
        }
    }
}
