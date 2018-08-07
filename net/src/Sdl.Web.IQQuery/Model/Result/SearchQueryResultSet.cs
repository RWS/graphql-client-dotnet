using System.Collections.Generic;
using Sdl.Web.IQQuery.API;

namespace Sdl.Web.IQQuery.Model.Result
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
