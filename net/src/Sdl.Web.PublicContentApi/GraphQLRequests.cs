using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sdl.Web.GraphQLClient.Request;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.PublicContentApi.Utils;

namespace Sdl.Web.PublicContentApi
{
    public static class GraphQLRequests
    {
        public static GraphQLRequest GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
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

        public static GraphQLRequest GetBinaryComponent(ContentNamespace ns, int publicationId, string url,
           IContextData contextData, IContextData globalContextData) => new GraphQLRequest
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

        public static GraphQLRequest GetBinaryComponent(CmUri cmUri,
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

        public static GraphQLRequest ExecuteItemQuery(InputItemFilter filter, InputSortParam sort, IPagination pagination,
          IContextData contextData, IContextData globaContextData, string customMetaFilter, bool renderContent)
        {
            if (contextData == null)
                contextData = new ContextData();

            // Dynamically build our item query based on the filter(s) being used.
            string query = Queries.Load("ItemQuery", false);

            // We only include the fragments that will be required based on the item types in the
            // input item filter
            if (filter.ItemTypes != null)
            {
                string fragmentList = filter.ItemTypes.Select(itemType
                    => $"{Enum.GetName(typeof(ContentModel.ItemType), itemType).Capitialize()}Fields").Aggregate(string.Empty, (current, fragment) => current + $"...{fragment}\n");
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

        public static GraphQLRequest GetPublication(ContentNamespace ns, int publicationId,
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

        public static GraphQLRequest ResolvePageLink(ContentNamespace ns, int publicationId, int pageId) => new GraphQLRequest
        {
            Query = Queries.Load("ResolvePageLink", true),
            Variables = new Dictionary<string, object>
            {
                {"namespaceId", ns},
                {"publicationId", publicationId},
                {"pageId", pageId}
            }
        };

        public static GraphQLRequest ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId, int? sourcePageId,
            int? excludeComponentTemplateId) => new GraphQLRequest
            {
                Query = Queries.Load("ResolveComponentLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"targetComponentId", componentId},
                    {"sourcePageId", sourcePageId},
                    {"excludeComponentTemplateId", excludeComponentTemplateId}
                }
            };

        public static GraphQLRequest ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
            string variantId) => new GraphQLRequest
            {
                Query = Queries.Load("ResolveBinaryLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"variantId", variantId}
                }
            };

        public static GraphQLRequest ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId,
            int componentId,
            int templateId) => new GraphQLRequest
            {
                Query = Queries.Load("ResolveDynamicComponentLink", true),
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"targetPageId", pageId},
                    {"targetComponentId", componentId},
                    {"targetTemplateId", templateId}
                }
            };

        public static GraphQLRequest GetPublicationMapping(ContentNamespace ns, string url) => new GraphQLRequest
        {
            Query = Queries.Load("PublicationMapping", true),
            Variables = new Dictionary<string, object>
            {
                {"namespaceId", ns},
                {"siteUrl", url}
            }
        };

        private static IContextData MergeContextData(IContextData localContextData, IContextData globalContextData)
        {
            if(localContextData == null && globalContextData == null)
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
         => query.Replace("@customMetaArgs", string.IsNullOrEmpty(customMetaFilter) ? "" : $"(filter: \"{customMetaFilter})\")");

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
    }
}
