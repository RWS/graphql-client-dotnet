namespace Sdl.Tridion.Api.Client.ContentModel
{
    /// <summary>
    /// Cursor-Based Pagination
    /// </summary>
    public class Pagination : IPagination
    {
        /// <summary>
        /// Return the first n number of results
        /// </summary>
        public int First { get; set; } = -1;

        /// <summary>
        /// Start returning results after the specified cursor. 
        /// <remarks>Place your cursor on edges in your query.</remarks>
        /// </summary>
        public string After { get; set; }
    }
}
