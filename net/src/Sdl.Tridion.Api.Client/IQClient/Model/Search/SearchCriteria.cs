using Sdl.Tridion.Api.IQQuery.API;

namespace Sdl.Tridion.Api.IQQuery.Model.Search
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
