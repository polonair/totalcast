using System;

namespace tc2
{
    interface IUi
    {
        event EventHandler<LinkInfoEventArgs> LinkGotten;
        event EventHandler<LinkInfoEventArgs> Subscribe;
        event EventHandler<LinkInfoEventArgs> UnSubscribe;
    }
}
