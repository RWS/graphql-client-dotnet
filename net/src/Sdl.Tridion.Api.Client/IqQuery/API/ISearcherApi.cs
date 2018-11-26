using System.Threading;
using System.Threading.Tasks;

namespace Sdl.Tridion.Api.IqQuery
{
    public interface ISearcherApi<T, R> where T : IQueryResultData<R> where R : IQueryResult
    {
        /// <summary>
        /// Sets the result filter.
        /// </summary>
        /// <param name="filter"the ResultFilter</param>
        /// <returns>SearchApi instancer</returns>
        ISearcherApi<T,R> WithResultFilter(IResultFilter filter);

        /// <summary>
        /// Sets the index name to search for. Defaults to the index given in udp-client-conf.xml TODO
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns>SearchApi instancer</returns>
        ISearcherApi<T,R> WithIndexName(string indexName);     

        /// <summary>
        /// Searches items based on the given criteria.
        /// </summary>
        /// <param name="criteria">Search Criteria</param>
        /// <returns>QueryResultData object.</returns>
        T Search(ICriteria criteria);

        /// <summary>
        /// Searches items based on the given criteria.
        /// </summary>
        /// <param name="criteria">Search Criteria</param>
        /// <returns>QueryResultData object.</returns>
        Task<T> SearchAsync(ICriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Searches items based on the given raw query.
        /// </summary>
        /// <param name="query">raw query</param>
        /// <returns>QueryResultData object.</returns>
        T Search(string query);

        /// <summary>
        /// Searches items based on the given raw query.
        /// </summary>
        /// <param name="query">raw query</param>
        /// <returns>QueryResultData object.</returns>
        Task<T> SearchAsync(string query, CancellationToken cancellationToken);
    }   
}
