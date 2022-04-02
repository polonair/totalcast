using System;

namespace tc2
{
    class GetEventArgs : EventArgs
    {
        public string Link { get; internal set; }
        public NetworkResult Result { get; internal set; }
        public int TimeOut { get; internal set; }
    }
}
