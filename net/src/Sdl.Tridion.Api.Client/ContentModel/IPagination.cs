namespace Sdl.Tridion.Api.Client.ContentModel
{
    /// <summary>
    /// Cursor-Based Pagination
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// Return the first n number of results
        /// </summary>
        int First { get; set; }

        /// <summary>
        /// Start returning results after the specified cursor. 
        /// <remarks>Place your cursor on edges in your query.</remarks>
        /// </summary>
        string After { get; set; }
    }
}
