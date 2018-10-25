using System;
using System.Collections.Generic;
using System.Linq;

namespace Sdl.Tridion.Api.Http.Client.Utils
{
    /// <summary>
    /// Uri Creator
    /// 
    /// Utility class for building full absolute Uris from parts of paths and query parameters.
    /// </summary>
    public class UriCreator
    {
        private readonly List<KeyValuePair<string, object>> _queryParams = new List<KeyValuePair<string, object>>();
        private readonly Uri _baseUri;
        private string _path;
        public static UriCreator FromString(string baseUri) => new UriCreator(new Uri(baseUri));
        public static UriCreator FromUri(Uri baseUri) => new UriCreator(baseUri);

        protected UriCreator(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public UriCreator WithPath(string path)
        {
            _path = !string.IsNullOrEmpty(_path) ? $"{_path.TrimEnd('/')}/{path.TrimStart('/')}" : path;
            return this;
        }

        public UriCreator WithQueryParam(string key, object value)
        {
            _queryParams.Add(new KeyValuePair<string, object>(key, value));
            return this;
        }

        public UriCreator WithQueryParams(List<KeyValuePair<string, object>> parameters)
        {
            foreach (var x in parameters)
            {
                _queryParams.Add(new KeyValuePair<string, object>(x.Key, x.Value));
            }
            return this;
        }

        public Uri Build()
        {
            string path = CombinePath(_baseUri.AbsolutePath, _path);
            return _queryParams.Count > 0
                ? new Uri(_baseUri, path + "?" + string.Join("&", _queryParams.Select(x => x.Key + "=" + x.Value)))
                : new Uri(_baseUri, path);
        }

        private static string CombinePath(string path1, string path2)
        {
            string a = (path1 ?? string.Empty).TrimEnd('/');
            string b = (path2 ?? string.Empty).TrimStart('/');
            return !string.IsNullOrEmpty(b) ? $"{a}/{b}" : a;
        }
    }
}
