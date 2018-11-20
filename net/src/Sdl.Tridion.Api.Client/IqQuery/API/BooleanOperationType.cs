using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// Operation Type of a Query part.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BooleanOperationType
    {
        /// <summary>
        /// Unit Type
        /// </summary>
        [EnumMember(Value = "UNIT")]
        Unit,

        /// <summary>
        /// And Type (+)
        /// </summary>
        [EnumMember(Value = "AND")]
        And,

        /// <summary>
        /// Or Type ( )
        /// </summary>
        [EnumMember(Value = "OR")]
        Or,

        /// <summary>
        /// Not Type (-)
        /// </summary>
        [EnumMember(Value = "NOT")]
        Not
    }
}
