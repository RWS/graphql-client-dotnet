using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Sdl.Tridion.Api.GraphQL.Client.Request;
using Sdl.Tridion.Api.Http.Client.Utils;
using Sdl.Tridion.Api.Client.ContentModel;
using Sdl.Tridion.Api.Client.Utils;

namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// QueryBuilder
    /// </summary>
    public class QueryBuilder
    {
        private StringBuilder _query;
        private Dictionary<string, object> _variables;
        private List<JsonConverter> _convertors;
        private IContextData _contextData;
        private string _customMetaFilter;
        private string _languageFilter;
        private string _operationName;
        private int _descendantLevels;
        private ContentIncludeMode _contentIncludeMode  = ContentIncludeMode.Exclude;

        public QueryBuilder()
        {
            _query = new StringBuilder();
        }

        public QueryBuilder(string query)
        {
            _query = new StringBuilder(query);
        }

        public QueryBuilder WithQuery(string query)
        {
            _query.Append(query);
            return this;
        }

        public QueryBuilder WithQueryResource(string queryResourceName, bool loadIncludedFragments)
        {
            _query.Append(Queries.Load(queryResourceName, loadIncludedFragments));
            return this;
        }

        public QueryBuilder WithCustomMetaFilter(string customMetaFilter)
        {
            _customMetaFilter = customMetaFilter;
            return this;
        }
        public QueryBuilder WithLanguageFilter(string languageFilter)
        {
            _languageFilter = languageFilter;
            return this;
        }

        public QueryBuilder WithOperationName(string operationName)
        {
            _operationName = operationName;
            return this;
        }

        public QueryBuilder WithConvertor(JsonConverter convertor)
        {
            if(_convertors == null) _convertors = new List<JsonConverter>();
            _convertors.Add(convertor);
            return this;
        }

        public QueryBuilder WithPublicationId(int publicationId) => WithVariable("publicationId", publicationId);

        public QueryBuilder WithPageId(int pageId) => WithVariable("pageId", pageId);

        public QueryBuilder WithBinaryId(int binaryId) => WithVariable("binaryId", binaryId);

        public QueryBuilder WithUrl(string url) => WithVariable("url", UrlEncoding.UrlEncodeNonAscii(url));

        public QueryBuilder WithCmUri(CmUri cmUri)
            => WithVariable("cmUri", cmUri).WithNamespace(cmUri.Namespace).WithPublicationId(cmUri.PublicationId);

        public QueryBuilder WithNamespace(ContentNamespace ns) => WithVariable("namespaceId", ns);

        public QueryBuilder WithInputPublicationFilter(InputPublicationFilter filter)
            => WithVariable("filter", filter);

        public QueryBuilder WithInputItemFilter(InputItemFilter filter) => WithVariable("inputItemFilter", filter);

        public QueryBuilder WithInputSortParam(InputSortParam sort) => WithVariable("inputSortParam", sort);

        public QueryBuilder WithInputComponentPresentationFilter(InputComponentPresentationFilter filter)
            => WithVariable("filter", filter);

        public QueryBuilder WithPagination(IPagination pagination)
        {
            if (pagination == null) return this;
            WithVariable("first", pagination.First);
            WithVariable("after", pagination.After);
            return this;
        }

        public QueryBuilder WithRenderRelativeLink(bool renderRelativeLink)
            => WithVariable("renderRelativeLink", renderRelativeLink);

        public QueryBuilder WithContentIncludeMode(ContentIncludeMode contentIncludeMode)
        {
            _contentIncludeMode = contentIncludeMode;
            return this;
        }

        public QueryBuilder WithContextClaim(ClaimValue claim)
        {
            if (_contextData == null) _contextData = new ContextData();
            _contextData.ClaimValues.Add(claim);
            return this;
        }

        public QueryBuilder WithContextData(IContextData contextData)
        {
            _contextData = MergeContextData(_contextData, contextData);
            return this;
        }

        public QueryBuilder WithVariable(string name, object value)
        {
            if (value == null) return this;
            if (_variables == null) _variables = new Dictionary<string, object>();
            _variables.Add(name, value);
            return this;
        }

        public QueryBuilder WithDescendantLevels(int descendantLevels)
        {
            _descendantLevels = descendantLevels;
            return this;
        }

        public QueryBuilder ReplaceTag(string tagName, string value)
        {
            _query.Replace($"@{tagName}", value);
            return this;
        }

        public QueryBuilder LoadFragments()
        {
            _query = new StringBuilder(Queries.LoadFragments(_query.ToString()));
            return this;
        }

        public QueryBuilder WithIncludeRegion(string regionName, bool include)
        {
            _query =
                new StringBuilder(QueryHelpers.ParseIncludeRegions(_query.ToString(), regionName, include));
            return this;
        }

        public IGraphQLRequest Build()
        {
            // Replace tags in graphQL query.
            ReplaceTag("customMetaArgs",
                string.IsNullOrEmpty(_customMetaFilter) ? "" : $"(filter: \"{_customMetaFilter}\")");

            ReplaceTag("languageArgs",
                string.IsNullOrEmpty(_languageFilter) ? "" : $"(language: \"{_languageFilter}\")");

            ReplaceTag("renderContentArgs", 
                $"(renderContent: {(_contentIncludeMode == ContentIncludeMode.IncludeDataAndRender || _contentIncludeMode == ContentIncludeMode.IncludeJsonAndRender ? "true" : "false")})");

            if (_variables != null)
            {
                ReplaceTag("variantsArgs", _variables.ContainsKey("url") ? $"(url: \"{_variables["url"]}\")" : "");
            }

            var query = QueryHelpers.ParseIncludeRegions(_query.ToString(), "includeContent",
                _contentIncludeMode == ContentIncludeMode.IncludeData || _contentIncludeMode == ContentIncludeMode.IncludeDataAndRender);

            query = QueryHelpers.ParseIncludeRegions(query, "includeJsonContent",
                _contentIncludeMode == ContentIncludeMode.IncludeJson || _contentIncludeMode == ContentIncludeMode.IncludeJsonAndRender);

            QueryHelpers.ExpandRecursiveFragment(ref query, null, _descendantLevels);

            QueryHelpers.RemoveUnusedFragments(ref query);

            if (_contextData != null)
            {
                WithVariable("contextData", _contextData.ClaimValues);
            }

            return new GraphQLRequest
            {
                OperationName = _operationName,
                Query = query,
                Variables = _variables,
                Convertors = _convertors
            };
        }      

        private static IContextData MergeContextData(IContextData localContextData, IContextData globalContextData)
        {
            if (localContextData == null && globalContextData == null)
                return new ContextData();

            if (localContextData == null)
                return globalContextData;

            if (globalContextData == null)
                return localContextData;

            IContextData merged = new ContextData();

            var set = new HashSet<string>();

            for (int i = 0; i < localContextData.ClaimValues.Count; i++)
            {
                string uri = localContextData.ClaimValues[i].Uri;
                if (set.Contains(uri)) continue;
                set.Add(uri);
                merged.ClaimValues.Add(localContextData.ClaimValues[i]);
            }

            for (int i = 0; i < globalContextData.ClaimValues.Count; i++)
            {
                string uri = globalContextData.ClaimValues[i].Uri;
                if (set.Contains(uri)) continue;
                set.Add(uri);
                merged.ClaimValues.Add(globalContextData.ClaimValues[i]);
            }
           
            return merged;
        }
    }
}
