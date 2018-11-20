using System;

namespace Sdl.Tridion.Api.Client.Core
{
    /// <summary>
    /// Default implementation for logging
    /// </summary>
    public class NullLogger : ILogger
    {
        public void Trace(string messageFormat, params object[] parameters)
        {
            // do nothing
        }

        public void Debug(string messageFormat, params object[] parameters)
        {
            // do nothing
        }

        public void Info(string messageFormat, params object[] parameters)
        {
            // do nothing
        }

        public void Warn(string messageFormat, params object[] parameters)
        {
            // do nothing
        }

        public void Error(string messageFormat, params object[] parameters)
        {
            // do nothing
        }

        public void Error(Exception ex, string messageFormat, params object[] parameters)
        {
            // do nothing
        }

        public void Error(Exception ex)
        {
            // do nothing
        }

        public bool IsTracingEnabled { get; } = false;
        public bool IsDebugEnabled { get; } = false;
    }
}
