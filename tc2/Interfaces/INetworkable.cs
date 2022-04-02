using System;

namespace tc2
{
    interface INetworkable
    {
        event EventHandler<GetEventArgs> Get;
        event EventHandler<PostEventArgs> Post;
        event EventHandler<GetEventArgs> LongPoll;
    }
}
