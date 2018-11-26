using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery.Model.Result
{
    /// <summary>
    /// Search Query Result
    /// </summary>
    public class SearchQueryResult : IDefaultQueryResult
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string Author { get; set; }
        public string MajorVersion { get; set; }
        public string MinorVersion { get; set; }
        public int PublicationId { get; set; }
        public string PublicationTitle { get; set; }
        public string ItemType { get; set; }
        public string EntityName { get; set; }
        public string Content { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public Dictionary<string, object> Highlighted { get; set; }
    }
}
