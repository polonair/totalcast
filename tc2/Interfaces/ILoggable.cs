using System;

namespace tc2
{
    interface ILoggable
    {
        event EventHandler<LogEventArgs> Log;
    }
}
