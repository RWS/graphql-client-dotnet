using System.Collections.Generic;

namespace Sdl.Tridion.Api.Http.Client
{
    /// <summary>
    /// Http Query Parameters
    /// </summary>
    public class HttpQueryParams : List<KeyValuePair<string, object>>
    {
        public HttpQueryParams()
        {
        }

        public HttpQueryParams(HttpQueryParams queryParams)
        {
            foreach (var x in queryParams)
                Add(new KeyValuePair<string, object>(x.Key, x.Value));
        }

        public void Add(string name, object value) 
            => Add(new KeyValuePair<string, object>(name, value));
    }
}
