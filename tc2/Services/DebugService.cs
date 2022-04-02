using System;

namespace tc2
{
    class DebugService : IService, ILogger
    {
        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}]: {message}\n");
        }
    }
}
