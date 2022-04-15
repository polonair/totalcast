using System;
using System.Collections.Generic;

namespace tc2
{
    interface IProvider
    {
        event EventHandler<CheckMimeEventArgs> CheckMime;
        bool CanProcess(Channel channel);
        bool CanProcess(string link);
        IEnumerable<Item> GetPublishedItems(Channel channel);
        Content LoadContent(Item item);
        LinkInfo GetLinkInfo(string link);
    }
}
