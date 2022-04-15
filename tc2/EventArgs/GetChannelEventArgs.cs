using System;
using System.Collections.Generic;

namespace tc2
{
    class GetChannelEventArgs : EventArgs
    {
        public Func<Channel, long> Sorter { get; internal set; }
        public Channel Channel { get; set; } = null;
    }
}
