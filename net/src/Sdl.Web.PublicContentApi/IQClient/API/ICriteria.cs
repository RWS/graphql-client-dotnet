namespace Sdl.Web.IQQuery.API
{
    /// <summary>
    /// Search Criteria. Sent to the search index.
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// Gets the raw query.
        /// </summary>
        string RawQuery { get; }
    }
}
