using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery.Model.Result
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
