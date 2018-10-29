using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// Base Operation interface.
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Compiles this instance. Final step before sending to the search index.
        /// </summary>
        /// <returns>A Criteria object for sending to the Query Service.</returns>
        ICriteria Compile();

        /// <summary>
        /// Sorts results by the specified fields.
        /// </summary>
        /// <param name="fieldNames">fieldNames the field names to sort on</param>
        /// <returns>A query object as boolean operation.</returns>
        IBooleanOperation SortByAscending(List<string> fieldNames);

        /// <summary>
        /// Sorts results by the specified fields, descending.
        /// </summary>
        /// <param name="fieldNames">fieldNames the field names to sort on</param>
        /// <returns>A query object as boolean operation.</returns>
        IBooleanOperation SortByDescending(List<string> fieldNames);

        /// <summary>
        /// Sorts results by the specified string fields.
        /// </summary>
        /// <param name="fieldNames">fieldNames the field names to sort on</param>
        /// <returns>A query object as boolean operation.</returns>
        IBooleanOperation SortStringByAscending(List<string> fieldNames);

        /// <summary>
        /// Sorts results by the specified string fields, descending.
        /// </summary>
        /// <param name="fieldNames">fieldNames the field names to sort on</param>
        /// <returns>A query object as boolean operation.</returns>
        IBooleanOperation SortStringByDescending(List<string> fieldNames);
    }
}
