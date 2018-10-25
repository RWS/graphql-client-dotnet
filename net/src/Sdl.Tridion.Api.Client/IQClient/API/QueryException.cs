using System;

namespace Sdl.Tridion.Api.IQQuery.API
{
    /// <summary>
    /// Query Exception
    /// </summary>
    public class QueryException : Exception
    {
        public QueryException(string msg) : base(msg)
        {
        }

        public QueryException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}
