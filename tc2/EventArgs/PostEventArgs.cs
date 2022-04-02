using System;
using System.Net.Http;

namespace tc2
{
    class PostEventArgs : EventArgs
    {
        public string Link { get; internal set; }
        public NetworkResult Result { get; internal set; }
        public HttpContent Content { get; internal set; }
    }
}
