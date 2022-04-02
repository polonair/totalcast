using System;
using System.Collections.Generic;

namespace tc2
{
    class Channel
    {
        public int Id { get; set; }
        public string Service { get; internal set; }
        public string SourceId { get; internal set; }
        public DateTime LastUpdate { get; internal set; }
        public List<Item> Items { get; set; } = new();
    }
}
