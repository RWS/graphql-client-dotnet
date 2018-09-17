using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.HttpClient.Utils;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Predefined GraphQL requests for working with the Public Content Api
    /// </summary>
    public static class GraphQLRequests
    {
        public static GraphQLRequest Page(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
           IContextData contextData, IContextData globalContextData) => new GraphQLRequest
           {
               Query =
                   InjectRenderContentArgs(InjectCustomMetaFilter(Queries.Load("PageById", true), customMetaFilter), false),
               Variables = new Dictionary<string, object>
               {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
               }
           };

        public static GraphQLRequest Page(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
          IContextData contextData, IContextData globalContextData) => new GraphQLRequest
          {
              Query =
                  InjectRenderContentArgs(InjectCustomMetaFilter(Queries.Load("PageByUrl", true), customMetaFilter), false),
              Variables = new Dictionary<string, object>
              {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
              }
          };

        public static GraphQLRequest Page(ContentNamespace ns, int publicationId, CmUri cmUri, string customMetaFilter,
         IContextData contextData, IContextData globalContextData) => new GraphQLRequest
         {
             Query =
                 InjectRenderContentArgs(InjectCustomMetaFilter(Queries.Load("PageByCmUri", true), customMetaFilter), false),
             Variables = new Dictionary<string, object>
             {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"cmUri", cmUri},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
             }
         };

        public static GraphQLRequest BinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData, IContextData globalContextData) => new GraphQLRequest
            {
                Query =
                    InjectVariantsArgs(InjectCustomMetaFilter(Queries.Load("BinaryComponentById", true), null), null),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };

        public static GraphQLRequest BinaryComponent(ContentNamespace ns, int publicationId, string url,
            IContextData contextData, IContextData globalContextData)
        {
            url = UrlEncoding.UrlEncodeNonAscii(url);
            return new GraphQLRequest
            {
                Query =
                    InjectVariantsArgs(InjectCustomMetaFilter(Queries.Load("BinaryComponentByUrl", true), null), url),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };
        }

        public static GraphQLRequest BinaryComponent(CmUri cmUri,
            IContextData contextData, IContextData globalContextData) => new GraphQLRequest
            {
                Query =
                    InjectVariantsArgs(InjectCustomMetaFilter(Queries.Load("BinaryComponentByCmUri", true), null), null),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", cmUri.Namespace},
                    {"publicationId", cmUri.PublicationId},
                    {"cmUri", cmUri.ToString()},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };

        public static GraphQLRequest ExecuteItemQuery(InputItemFilter filter, InputSortParam sort,
            IPagination pagination,
            IContextData contextData, IContextData globaContextData, string customMetaFilter, bool renderContent)
        {
            // Dynamically build our item query based on the filter(s) being used.
            string query = Queries.Load("ItemQuery", false);

            // We only include the fragments that will be required based on the item types in the
            // input item filter
            if (filter.ItemTypes != null)
            {
                string fragmentList = filter.ItemTypes.Select(itemType
                    => $"{Enum.GetName(typeof (ContentModel.ItemType), itemType).Capitialize()}Fields")
                    .Aggregate(string.Empty, (current, fragment) => current + $"...{fragment}\n");
                // Just a quick and easy way to replace markers in our queries with vars here.
                query = query.Replace("@fragmentList", fragmentList);
                query = Queries.LoadFragments(query);
            }

            query = InjectCustomMetaFilter(query, customMetaFilter);
            query = InjectRenderContentArgs(query, renderContent);
            return new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"filter", filter},
                    {"sort", sort},
                    {"contextData", MergeContextData(contextData, globaContextData).ClaimValues}
                },
                Convertors = new List<JsonConverter> {new ItemConvertor()}
            };
        }

        public static GraphQLRequest Publication(ContentNamespace ns, int publicationId,
            IContextData contextData, IContextData globalContextData, string customMetaFilter) => new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("Publication", true), customMetaFilter),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };

        public static GraphQLRequest Publications(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter,
            IContextData contextData, IContextData globalContextData, string customMetaFilter) => new GraphQLRequest
            {
                Query = InjectCustomMetaFilter(Queries.Load("Publications", true), customMetaFilter),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"filter", filter},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };

        public static GraphQLRequest ResolvePageLink(ContentNamespace ns, int publicationId, int pageId,
            bool renderRelativeLink)
            => new GraphQLRequest
            {
                Query = Queries.Load("ResolvePageLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"renderRelativeLink", renderRelativeLink}
                }
            };

        public static GraphQLRequest ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId,
            int? excludeComponentTemplateId, bool renderRelativeLink) => new GraphQLRequest
            {
                Query = Queries.Load("ResolveComponentLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"targetComponentId", componentId},
                    {"sourcePageId", sourcePageId},
                    {"excludeComponentTemplateId", excludeComponentTemplateId},
                    {"renderRelativeLink", renderRelativeLink}
                }
            };

        public static GraphQLRequest ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
            string variantId, bool renderRelativeLink) => new GraphQLRequest
            {
                Query = Queries.Load("ResolveBinaryLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"variantId", variantId},
                    {"renderRelativeLink", renderRelativeLink}
                }
            };

        public static GraphQLRequest ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId,
            int componentId,
            int templateId, bool renderRelativeLink) => new GraphQLRequest
            {
                Query = Queries.Load("ResolveDynamicComponentLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"targetPageId", pageId},
                    {"targetComponentId", componentId},
                    {"targetTemplateId", templateId},
                    {"renderRelativeLink", renderRelativeLink}
                }
            };

        public static GraphQLRequest PublicationMapping(ContentNamespace ns, string url) => new GraphQLRequest
        {
            Query = Queries.Load("PublicationMapping", true),
            Variables = new Dictionary<string, object>
            {
                {"namespaceId", ns},
                {"siteUrl", url}
            }
        };

        public static GraphQLRequest PageModelData(ContentNamespace ns, int publicationId, int pageId,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            IContextData globalContextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            return new GraphQLRequest
            {
                Query = InjectRenderContentArgs(Queries.Load("PageModelById", true), renderContent),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };
        }

        public static GraphQLRequest PageModelData(ContentNamespace ns, int publicationId, string url,
            ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, bool renderContent, IContextData contextData,
            IContextData globalContextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            return new GraphQLRequest
            {
                Query = InjectRenderContentArgs(Queries.Load("PageModelByUrl", true), renderContent),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", UrlEncoding.UrlEncodeNonAscii(url)},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                },
                OperationName = "page"
            };
        }

        public static GraphQLRequest EntityModelData(ContentNamespace ns, int publicationId, int entityId,
            int templateId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, bool renderContent, IContextData contextData,
            IContextData globalContextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, dcpType);
            return new GraphQLRequest
            {
                Query = InjectRenderContentArgs(Queries.Load("EntityModelById", true), renderContent),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"componentId", entityId},
                    {"templateId", templateId},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                }
            };
        }

        public static GraphQLRequest Sitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData, IContextData globalContextData)
        {
            string query = Queries.Load("Sitemap", true);
            QueryHelpers.ExpandRecursiveFragment(ref query, null, descendantLevels);
            return new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                },
                Convertors = new List<JsonConverter> {new TaxonomyItemConvertor()}
            };
        }

        public static GraphQLRequest SitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, bool includeAncestors,
            IContextData contextData, IContextData globalContextData)
        {
            string query;
            if (descendantLevels == 0)
            {
                query = Queries.Load("SitemapSubtreeNoRecurse", true);                
            }
            else
            {
                query = Queries.Load("SitemapSubtree", true);
                QueryHelpers.ExpandRecursiveFragment(ref query, null, descendantLevels);
            }

            return new GraphQLRequest
            {
                Query = query,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"taxonomyNodeId", taxonomyNodeId},
                    {"includeAncestors", includeAncestors},
                    {"contextData", MergeContextData(contextData, globalContextData).ClaimValues}
                },
                Convertors = new List<JsonConverter> {new TaxonomyItemConvertor()}
            };
        }

        #region Query Builder Helpers

        private static IContextData MergeContextData(IContextData localContextData, IContextData globalContextData)
        {
            if (localContextData == null && globalContextData == null)
                return new ContextData();

            if (localContextData == null)
                return globalContextData;

            if (globalContextData == null)
                return localContextData;

            IContextData merged = new ContextData();
            merged.ClaimValues = globalContextData.ClaimValues.Concat(localContextData.ClaimValues).ToList();
            return merged;
        }

        private static string InjectCustomMetaFilter(string query, string customMetaFilter)
            =>
                query.Replace("@customMetaArgs",
                    string.IsNullOrEmpty(customMetaFilter) ? "" : $"(filter: \"{customMetaFilter}\")");

        private static string InjectRenderContentArgs(string query, bool renderContent)
            => query.Replace("@renderContentArgs", $"(renderContent: {(renderContent ? "true" : "false")})");

        private static string InjectVariantsArgs(string query, string url)
            => query.Replace("@variantsArgs", !string.IsNullOrEmpty(url) ? $"(url: \"{url}\")" : "");

        private static LinkType GetLinkType(CmUri cmUri, bool resolveToBinary)
        {
            if (cmUri.ItemType == ItemType.Page) return LinkType.PAGE;
            if (cmUri.ItemType == ItemType.Component && resolveToBinary) return LinkType.BINARY;
            return LinkType.COMPONENT;
        }

        private static ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ContentType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (ContentType), contentType)
        };

        private static ClaimValue CreateClaim(DataModelType dataModelType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DataModelType), dataModelType)
        };

        private static ClaimValue CreateClaim(PageInclusion pageInclusion) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (PageInclusion), pageInclusion)
        };

        private static ClaimValue CreateClaim(DcpType dcpType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.EntityDcpType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DcpType), dcpType)
        };

        private static void UpdateContextData(ref IContextData contextData, ContentType contentType,
            DataModelType dataModelType, PageInclusion pageInclusion)
        {
            if (contextData == null) contextData = new ContextData();
            contextData.ClaimValues.Add(CreateClaim(contentType));
            contextData.ClaimValues.Add(CreateClaim(dataModelType));
            contextData.ClaimValues.Add(CreateClaim(pageInclusion));
        }

        private static void UpdateContextData(ref IContextData contextData, ContentType contentType,
            DataModelType dataModelType, DcpType dcpType)
        {
            if (contextData == null) contextData = new ContextData();
            contextData.ClaimValues.Add(CreateClaim(contentType));
            contextData.ClaimValues.Add(CreateClaim(dataModelType));
            contextData.ClaimValues.Add(CreateClaim(dcpType));
        }

        #endregion
    }
}