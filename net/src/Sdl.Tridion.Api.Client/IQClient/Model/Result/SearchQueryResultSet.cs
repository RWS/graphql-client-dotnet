using System.Collections.Generic;
using Sdl.Tridion.Api.IQQuery.API;

namespace Sdl.Tridion.Api.IQQuery.Model.Result
{
    /// <summary>
    /// Search Query Result Set
    /// </summary>
    public class SearchQueryResultSet : IQueryResultData<SearchQueryResult>
    {
        public int Hits { get; set; }
        public IList<SearchQueryResult> QueryResults { get; set; }
    }
}
