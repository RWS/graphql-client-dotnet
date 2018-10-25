using System;

namespace Sdl.Tridion.Api.IQQuery.API
{
    public class QueryProviderException : Exception
    {
        public QueryProviderException(string msg) : base(msg)
        {
        }

        public QueryProviderException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}
