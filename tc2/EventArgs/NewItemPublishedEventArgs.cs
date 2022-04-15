using System;
using System.Collections.Generic;

namespace tc2
{
    class NewItemPublishedEventArgs : EventArgs
    {
        public IEnumerable<Item> Items { get; internal set; }
    }
}
