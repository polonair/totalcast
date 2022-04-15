using System;

namespace tc2
{
    class LinkInfoEventArgs : EventArgs
    {
        public string Link { get; internal set; }
        public LinkInfo LinkInfo { get; internal set; }
    }
}
