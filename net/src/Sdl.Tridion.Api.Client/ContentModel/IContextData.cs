using System.Collections.Generic;

namespace Sdl.Tridion.Api.Client.ContentModel
{
    /// <summary>
    /// Context Data
    /// </summary>
    public interface IContextData
    {
        /// <summary>
        /// List of claim values to pass to query.
        /// </summary>
        List<ClaimValue> ClaimValues { get; set; }
    }
}
