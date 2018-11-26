using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// Default Result fields.
    /// </summary>
    public interface IDefaultQueryResult : IQueryResult
    {
        /// <summary>
        /// Represents a unique key.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The Url.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// The creation date.
        /// </summary>
        string CreatedDate { get; set; }

        /// <summary>
        /// The modification date.
        /// </summary>
        string ModifiedDate { get; set; }

        /// <summary>
        /// The author.
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// The major version.
        /// </summary>
        string MajorVersion { get; set; }

        /// <summary>
        /// The minor version.
        /// </summary>
        string MinorVersion { get; set; }

        /// <summary>
        /// The publication id.
        /// </summary>
        int PublicationId { get; set; }

        /// <summary>
        /// The publication title.
        /// </summary>
        string PublicationTitle { get; set; }

        /// <summary>
        /// Item Type.
        /// </summary>
        string ItemType { get; set; }

        /// <summary>
        /// Entity mapping name.
        /// </summary>
        string EntityName { get; set; }

        /// <summary>
        /// Locale of the entity. Useful for indexing the same thing with different analyzers by Locale.
        /// </summary>

        //Locale Locale { get; set; }

        /// <summary>
        /// The content on which analysis takes place.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Fields
        /// </summary>
        Dictionary<string, object> Fields { get; set; }

        /// <summary>
        /// Highlighted Fields
        /// </summary>
        Dictionary<string, object> Highlighted { get; set; }
    }
}
