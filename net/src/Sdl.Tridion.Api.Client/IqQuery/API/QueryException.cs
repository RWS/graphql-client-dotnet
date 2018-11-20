using System;

namespace Sdl.Tridion.Api.IqQuery
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
