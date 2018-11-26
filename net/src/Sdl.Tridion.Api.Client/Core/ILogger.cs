using System;

namespace Sdl.Tridion.Api.Client.Core
{
    /// <summary>
    /// ILogger interface used for logging.
    /// </summary>
    public interface ILogger
    {        
        void Trace(string messageFormat, params object[] parameters);
        void Debug(string messageFormat, params object[] parameters);
        void Info(string messageFormat, params object[] parameters);
        void Warn(string messageFormat, params object[] parameters);
        void Error(string messageFormat, params object[] parameters);
        void Error(Exception ex, string messageFormat, params object[] parameters);
        void Error(Exception ex);

        bool IsTracingEnabled { get; }
        bool IsDebugEnabled { get; }
    }
}
