using System;

namespace tc2
{
    class LogEventArgs : EventArgs
    {
        public string Message { get; internal set; }
        public static implicit operator LogEventArgs(string message) => new LogEventArgs() { Message = message };
    }
}
