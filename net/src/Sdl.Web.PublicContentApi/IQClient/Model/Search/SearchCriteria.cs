using Sdl.Web.IQQuery.API;

namespace Sdl.Web.IQQuery.Model.Search
{
    /// <summary>
    /// Default implementation for Criteria.
    /// </summary>
    public class SearchCriteria : ICriteria
    {
        public string RawQuery { get; }

        public SearchCriteria(string rawQuery)
        {
            RawQuery = rawQuery;
        }
    }
}
