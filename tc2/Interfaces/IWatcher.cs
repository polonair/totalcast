using System;

namespace tc2
{
    interface IWatcher
    {
        event EventHandler BeginWatching;
        event EventHandler<GetChannelEventArgs> GetChannel;
        event EventHandler<GetItemsEventArgs> GetPublishedItems;
        event EventHandler<NewItemPublishedEventArgs> NewItemPublished;
    }
}
