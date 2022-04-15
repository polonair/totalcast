using System;
using System.IO;
using System.Linq;

namespace tc2
{
    class Logger : IService, ILogger, IConfigurable
    {
        public string LogFileName { get; private set; }

        public void Configure(ServiceParam[] config)
        {
            config.ToList().ForEach(p =>
            {
                switch (p.key)
                {
                    case "logFileName": this.LogFileName = (string)p.value; break;
                }
            });
        }
        public void Log(string message)
        {
            File.AppendAllText(this.LogFileName, $"[{DateTime.Now}]: {message}\n");
        }
    }
}
