using System.IO;
using System.Net.Http;

namespace tc2
{
    class NetworkResult
    {
        private HttpContent content;

        public NetworkResult(HttpContent content)
        {
            this.content = content;
        }
        internal string ReadAsString() => this.content.ReadAsStringAsync().Result;
        internal Stream ReadAsStream() => this.content.ReadAsStreamAsync().Result;
    }
}
