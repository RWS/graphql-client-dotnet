using System;
using System.Linq;
using Sdl.Tridion.Api.GraphQL.Client.Request;
using Sdl.Tridion.Api.Client.ContentModel;
using Sdl.Tridion.Api.Client.Utils;
using System.Collections.Generic;

namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// Predefined GraphQL requests for working with the Public Content Api
    /// </summary>
    public static class GraphQLRequests
    {
        public static IGraphQLRequest ComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("ComponentPresentation", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithVariable("componentId", componentId)
                    .WithVariable("templateId", templateId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithConvertor(new ItemConvertor())
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest ComponentPresentations(ContentNamespace ns, int publicationId,
            InputComponentPresentationFilter filter, InputSortParam sort, IPagination pagination,
            string customMetaFilter,
            ContentIncludeMode contentIncludeMode, IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("ComponentPresentations", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithInputComponentPresentationFilter(filter)
                    .WithInputSortParam(sort)
                    .WithPagination(pagination)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .WithConvertor(new ItemConvertor())
                    .Build();

        public static IGraphQLRequest Page(ContentNamespace ns, int publicationId, int pageId, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageById", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithPageId(pageId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Page(ContentNamespace ns, int publicationId, string url, string customMetaFilter,
            ContentIncludeMode contentIncludeMode,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageByUrl", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithUrl(url)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Page(CmUri cmUri, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageByCmUri", true).WithCmUri(cmUri)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Pages(ContentNamespace ns, IPagination pagination, string url,
            string customMetaFilter, ContentIncludeMode contentIncludeMode, IContextData contextData,
            IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PagesByUrl", true)
                    .WithNamespace(ns)
                    .WithUrl(url)
                    .WithPagination(pagination)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest BinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            string customMetaFilter, IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("BinaryComponentById", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithBinaryId(binaryId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest BinaryComponent(ContentNamespace ns, int publicationId, string url,
            string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("BinaryComponentByUrl", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithUrl(url)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest BinaryComponent(CmUri cmUri, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("BinaryComponentByCmUri", true).WithCmUri(cmUri)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest ExecuteItemQuery(InputItemFilter filter, InputSortParam sort,
            IPagination pagination, string customMetaFilter, ContentIncludeMode contentIncludeMode,
            bool includeContainerItems,
            IContextData contextData, IContextData globaContextData)
        {
            QueryBuilder builder = new QueryBuilder().WithQueryResource("ItemQuery", false);

            // We only include the fragments that will be required based on the item types in the
            // input item filter
            if (filter.ItemTypes != null)
            {
                string fragmentList = filter.ItemTypes.Select(itemType
                    => $"{Enum.GetName(typeof (ContentModel.FilterItemType), itemType).Capitialize()}Fields")
                    .Aggregate(string.Empty, (current, fragment) => current + $"...{fragment}\n");
                // Just a quick and easy way to replace markers in our queries with vars here.
                builder.ReplaceTag("fragmentList", fragmentList);
                builder.LoadFragments();
            }

            return builder.WithIncludeRegion("includeContainerItems", includeContainerItems).
                WithPagination(pagination).
                WithCustomMetaFilter(customMetaFilter).
                WithContentIncludeMode(contentIncludeMode).
                WithInputItemFilter(filter).
                WithInputSortParam(sort).
                WithContextData(contextData).
                WithContextData(globaContextData).
                WithConvertor(new ItemConvertor()).
                Build();
        }

        public static IGraphQLRequest BuildExternalItemQuery(string eclUri, string itemType, List<string> itemFields)
        {
            QueryBuilder builder = new QueryBuilder().WithQueryResource("ExternalItemQuery", false);

            builder.ReplaceTag("eclUri", eclUri);
            builder.ReplaceTag("itemType", itemType);
            builder.ReplaceTag("itemFields", string.Join(Environment.NewLine, itemFields));

            return builder.Build();
        }

        public static IGraphQLRequest Publication(ContentNamespace ns, int publicationId, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("Publication", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest Publications(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter filter, string customMetaFilter,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("Publications", true).WithNamespace(ns).WithPagination(pagination)
                    .WithInputPublicationFilter(filter)
                    .WithCustomMetaFilter(customMetaFilter)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest ResolvePageLink(ContentNamespace ns, int publicationId, int pageId,
            bool renderRelativeLink)
            => new QueryBuilder().WithQueryResource("ResolvePageLink", true)
                .WithNamespace(ns)
                .WithPublicationId(publicationId)
                .WithPageId(pageId)
                .WithRenderRelativeLink(renderRelativeLink).Build();

        public static IGraphQLRequest ResolveComponentLink(ContentNamespace ns, int publicationId, int componentId,
            int? sourcePageId, int? excludeComponentTemplateId, bool renderRelativeLink) =>
                new QueryBuilder().WithQueryResource("ResolveComponentLink", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithRenderRelativeLink(renderRelativeLink)
                    .WithVariable("targetComponentId", componentId)
                    .WithVariable("sourcePageId", sourcePageId)
                    .WithVariable("excludeComponentTemplateId", excludeComponentTemplateId)
                    .Build();

        public static IGraphQLRequest ResolveBinaryLink(ContentNamespace ns, int publicationId, int binaryId,
            string variantId, bool renderRelativeLink) =>
                new QueryBuilder().WithQueryResource("ResolveBinaryLink", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithRenderRelativeLink(renderRelativeLink)
                    .WithBinaryId(binaryId)
                    .WithVariable("variantId", variantId)
                    .Build();

        public static IGraphQLRequest ResolveDynamicComponentLink(ContentNamespace ns, int publicationId, int pageId,
            int componentId,
            int templateId, bool renderRelativeLink) =>
                new QueryBuilder().WithQueryResource("ResolveDynamicComponentLink", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithRenderRelativeLink(renderRelativeLink)
                    .WithVariable("targetPageId", pageId)
                    .WithVariable("targetComponentId", componentId)
                    .WithVariable("targetTemplateId", templateId)
                    .Build();

        public static IGraphQLRequest PublicationMapping(ContentNamespace ns, string url) =>
            new QueryBuilder().WithQueryResource("PublicationMapping", true)
                .WithNamespace(ns)
                .WithVariable("siteUrl", url)
                .Build();

        public static IGraphQLRequest PageModelData(ContentNamespace ns, int publicationId, int pageId,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode, IContextData contextData,
            IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("PageModelById", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithPageId(pageId)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextClaim(CreateClaim(pageInclusion))
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest PageModelData(ContentNamespace ns, int publicationId, string url,
            PageInclusion pageInclusion, ContentIncludeMode contentIncludeMode,
            IContextData contextData, IContextData globalContextData) =>

                new QueryBuilder().WithQueryResource("PageModelByUrl", true)
                    .WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithUrl(url)
                    .WithContentIncludeMode(contentIncludeMode)
                    .WithContextClaim(CreateClaim(pageInclusion))
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .Build();

        public static IGraphQLRequest EntityModelData(ContentNamespace ns, int publicationId, int entityId,
            int templateId, ContentIncludeMode contentIncludeMode,
            IContextData contextData, IContextData globalContextData) =>
                new QueryBuilder().WithQueryResource("EntityModelById", true).
                    WithNamespace(ns).
                    WithPublicationId(publicationId).
                    WithVariable("componentId", entityId).
                    WithVariable("templateId", templateId).
                    WithContentIncludeMode(contentIncludeMode).
                    WithContextClaim(CreateClaim(DcpType.DEFAULT)).
                    WithContextData(contextData).
                    WithContextData(globalContextData).
                    Build();

        public static IGraphQLRequest Sitemap(ContentNamespace ns, int publicationId, int descendantLevels,
            IContextData contextData, IContextData globalContextData)
            => new QueryBuilder().WithQueryResource("Sitemap", true)
                .WithNamespace(ns)
                .WithPublicationId(publicationId)
                .WithDescendantLevels(descendantLevels)
                .WithContextData(contextData)
                .WithContextData(globalContextData)
                .WithConvertor(new TaxonomyItemConvertor())
                .Build();

        public static IGraphQLRequest SitemapSubtree(ContentNamespace ns, int publicationId, string taxonomyNodeId,
            int descendantLevels, Ancestor ancestor,
            IContextData contextData, IContextData globalContextData)
        {
            QueryBuilder builder =
                new QueryBuilder().WithQueryResource(
                    descendantLevels == 0 ? "SitemapSubtreeNoRecurse" : "SitemapSubtree", true);
            return
                builder.WithNamespace(ns)
                    .WithPublicationId(publicationId)
                    .WithVariable("taxonomyNodeId", taxonomyNodeId)
                    .WithVariable("ancestor", ancestor)
                    .WithContextData(contextData)
                    .WithContextData(globalContextData)
                    .WithDescendantLevels(descendantLevels)
                    .WithConvertor(new TaxonomyItemConvertor())
                    .Build();
        }

        public static IGraphQLRequest SearchByRawCriteria(string rawCritera, InputResultFilter resultFilter, IPagination pagination)
        {
            QueryBuilder builder =
                new QueryBuilder().WithQueryResource("SearchByRawCriteria", false);
                    
            return
                builder
                    .WithVariable("rawCriteria", rawCritera)
                    .WithVariable("inputResultFilter", resultFilter)
                    .WithPagination(pagination)
                    .Build();
        }

        public static IGraphQLRequest SearchByCriteria(InputCriteria criteria, InputResultFilter resultFilter, IPagination pagination)
        {
            QueryBuilder builder =
                new QueryBuilder().WithQueryResource("SearchByCriteria", false);

            return
                builder
                    .WithVariable("criteria", criteria)
                    .WithVariable("inputResultFilter", resultFilter)
                    .WithPagination(pagination)
                    .Build();
        }

        public static IGraphQLRequest FacetedSearch(InputCriteria criteria, InputFacets inputFacets, string language, InputResultFilter resultFilter, IPagination pagination)
        {
            QueryBuilder builder =
                new QueryBuilder().WithQueryResource("SearchByFaceted", false);

            return
                builder
                    .WithVariable("criteria", criteria)
                    .WithVariable("facets", inputFacets)
                    .WithVariable("inputResultFilter", resultFilter)
                    .WithPagination(pagination)
                    .WithLanguageFilter(language)
                    .Build();
        }

        public static IGraphQLRequest Suggest(string label, string langauage, bool fuzzy, bool used, string connectorId, IPagination pagination)
        {
            QueryBuilder builder =
                new QueryBuilder().WithQueryResource("Suggest", false);

            return
                builder
                    .WithVariable("label", label)
                    .WithVariable("langauage", langauage)
                    .WithVariable("fuzzy", fuzzy)
                    .WithVariable("used", used)
                    .WithVariable("connectorId", connectorId)
                    .WithPagination(pagination)
                    .Build();
        }

        #region Query Builder Helpers

        public static ClaimValue CreateClaim(ModelServiceLinkRendering linkRendering) => new ClaimValue
        {
            Uri = ClaimUris.ModelServiceLinkRendering,
            Type = ClaimValueType.BOOLEAN,
            Value = linkRendering == ModelServiceLinkRendering.Relative ? "true" : "false"
        };

        public static ClaimValue CreateClaim(TcdlLinkRendering linkRendering) => new ClaimValue
        {
            Uri = ClaimUris.TcdlLinkRendering,
            Type = ClaimValueType.BOOLEAN,
            Value = linkRendering == TcdlLinkRendering.Relative ? "true" : "false"
        };

        public static ClaimValue CreateClaimTcdlLinkUrlPrefix(string urlPrefix) => new ClaimValue
        {
            Uri = ClaimUris.TcdlLinkUrlPrefix,
            Type = ClaimValueType.STRING,
            Value = urlPrefix
        };

        public static ClaimValue CreateClaimTcdlBinaryLinkUrlPrefix(string urlPrefix) => new ClaimValue
        {
            Uri = ClaimUris.TcdlBinaryLinkUrlPrefix,
            Type = ClaimValueType.STRING,
            Value = urlPrefix
        };

        public static ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ClaimUris.ContentType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (ContentType), contentType)
        };

        public static ClaimValue CreateClaim(DataModelType dataModelType) => new ClaimValue
        {
            Uri = ClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DataModelType), dataModelType)
        };

        public static ClaimValue CreateClaim(PageInclusion pageInclusion) => new ClaimValue
        {
            Uri = ClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (PageInclusion), pageInclusion)
        };

        public static ClaimValue CreateClaim(DcpType dcpType) => new ClaimValue
        {
            Uri = ClaimUris.EntityDcpType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DcpType), dcpType)
        };

        #endregion
    }
}