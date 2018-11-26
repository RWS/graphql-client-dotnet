using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sdl.Tridion.Api.Client.ContentModel
{
    /// <summary>
    /// Context Data
    /// </summary>
    public class ContextData : IContextData
    {
        public ContextData()
        {
        }

        public ContextData(IContextData copy)
        {
            ClaimValues = new List<ClaimValue>(copy.ClaimValues);    
        }

        /// <summary>
        /// List of claim values to pass to query.
        /// </summary>
        public List<ClaimValue> ClaimValues { get; set; } = new List<ClaimValue>();
    }  
}
