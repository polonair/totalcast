using System;
using System.Collections.Generic;

namespace tc2
{
    interface IDatabase
    {
        void BeginDownload(Item item);
        void CannotDownload(Item item);
        void DownloadDone(Item item);
        Channel SelectChannel(Func<Channel, long> sorter);
        IEnumerable<Item> SelectLoadedChannels(Channel channel, int maxItems);
        void Subscribe(Channel c);
        bool IsSubscribed(string sourceId);
        void CleanPending();
    }
}
