using System;

namespace Sdl.Web.PublicContentApi.Exceptions
{
    public class PcaException : Exception
    {
        public PcaException(string msg) : base(msg)
        {
        }

        public PcaException(string msg, Exception ex) : base(msg, ex)
        {
        }
    }
}
