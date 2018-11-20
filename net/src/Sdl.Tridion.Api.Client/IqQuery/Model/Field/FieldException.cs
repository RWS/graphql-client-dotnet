using System;

namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Field Exception
    /// </summary>
    public class FieldException : Exception
    {
        public FieldException(string msg) : base(msg)
        {
        }

        public FieldException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}
