using System.Threading;
using System.Threading.Tasks;

namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// IQueryClient
    /// </summary>
    /// <typeparam name="T">Result Set type</typeparam>
    /// <typeparam name="R">Result type</typeparam>
    public interface IQueryClient<T,R> where T : IQueryResultData<R> where R : IQueryResult
    {
        /// <summary>
        /// Searches a document by Id.
        /// </summary>      
        /// <param name="index">Index name</param>
        /// <param name="id">Id</param>
        /// <returns>Results</returns>
        T SearchById(string index, string id);

        /// <summary>
        /// Searches a document by Id.
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="id">Id</param>
        /// <returns>Results</returns>
        Task<T> SearchByIdAsync(string index, string id, CancellationToken cancellationToken);

        /// <summary>
        /// Searches for documents by the specified criteria.
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="filter">Filter</param>
        /// <returns>Results</returns>
        T SearchWithCriteria(string index, string criteria, IResultFilter filter);

        /// <summary>
        /// Searches for documents by the specified criteria.
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="filter">Filter</param>
        /// <returns>Results</returns>
        Task<T> SearchWithCriteriaAsync(string index, string criteria, IResultFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Searches for documents by the specified criteria.
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        T SearchWithCriteria(string index, ICriteria criteria, IResultFilter filter);

        /// <summary>
        /// Searches for documents by the specified criteria.
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        Task<T> SearchWithCriteriaAsync(string index, ICriteria criteria, IResultFilter filter, CancellationToken cancellationToken);
    }
}
