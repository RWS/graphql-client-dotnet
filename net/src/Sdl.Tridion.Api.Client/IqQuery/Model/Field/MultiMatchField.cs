using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Multi Match Field.
    /// </summary>
    public class MultiMatchField : BaseField
    {
        public string Query { get; set; }

        public List<string> Fields { get; protected set; } = new List<string> { "content.*", "dynamic.*" };

        public MultiMatchField(bool negate, string query) : base(negate)
        {
            Query = query;
        }
        public MultiMatchField(bool negate, string query, List<string> fields) : this(negate, query)
        {
            Fields = fields;
        }
    }
}
