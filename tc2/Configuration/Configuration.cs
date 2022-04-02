using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace tc2
{
    class Configuration
    {
        public class Config
        {
            public ServiceData[] services;
        }

        private Config _Config;

        public Configuration(string path)
        {
            string content = File.ReadAllText(path);
            this._Config = JsonConvert.DeserializeObject<Config>(content);
        }
        public bool Contains(string name) => this._Config.services.Where(s => s.@class == name).FirstOrDefault() != null;
        public ServiceParam[] GetConfiguration(string name) => this._Config.services.Where(s => s.@class == name).FirstOrDefault().@params;
    }
}
