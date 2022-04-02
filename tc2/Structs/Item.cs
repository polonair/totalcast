using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace tc2
{
    class Item
    {
        class comparer : IEqualityComparer<Item>
        {
            public bool Equals(Item x, Item y) => x.SourceId == y.SourceId;
            public int GetHashCode([DisallowNull] Item obj) => obj.SourceId.GetHashCode();
        }
        public readonly static IEqualityComparer<Item> Comparer = new comparer();
        public int Id { get; set; }
        public string SourceId { get; internal set; }
        public string Title { get; internal set; }
        public string Author { get; internal set; }
        public string Description { get; internal set; }
        public string Link { get; internal set; }
        public Channel Channel { get; set; }
        public ItemState State { get; internal set; }
        public DateTime Published { get; internal set; }
    }
}
