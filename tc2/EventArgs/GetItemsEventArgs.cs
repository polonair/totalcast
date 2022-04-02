using System;
using System.Collections.Generic;

namespace tc2
{
    class GetItemsEventArgs : EventArgs
    {
        public Channel Channel { get; internal set; }
        public IEnumerable<Item> Items { get; internal set; }
        public int MaxItems { get; internal set; }
    }
}
