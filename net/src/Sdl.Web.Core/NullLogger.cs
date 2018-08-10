using System;

namespace Sdl.Web.Core
{
    public class NullLogger : ILogger
    {
        public void Trace(string messageFormat, params object[] parameters)
        {
           
        }

        public void Debug(string messageFormat, params object[] parameters)
        {
           
        }

        public void Info(string messageFormat, params object[] parameters)
        {
           
        }

        public void Warn(string messageFormat, params object[] parameters)
        {
           
        }

        public void Error(string messageFormat, params object[] parameters)
        {
           
        }

        public void Error(Exception ex, string messageFormat, params object[] parameters)
        {
           
        }

        public void Error(Exception ex)
        {
           
        }

        public bool IsTracingEnabled { get; } = false;
        public bool IsDebugEnabled { get; } = false;
    }
}
