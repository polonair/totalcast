using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace tc2
{
    class Watcher : IService, IWatcher, IRunnable, IConfigurable, ILoggable
    {
        public int WatchInterval { get; private set; }

        public event EventHandler BeginWatching;
        public event EventHandler<GetChannelEventArgs> GetChannel;
        public event EventHandler<GetItemsEventArgs> GetPublishedItems;
        public event EventHandler<NewItemPublishedEventArgs> NewItemPublished;
        public event EventHandler<LogEventArgs> Log;

        public void Configure(ServiceParam[] config)
        {
            config.ToList().ForEach(p =>
            {
                switch (p.key)
                {
                    case "watchInterval": this.WatchInterval = (int)(long)p.value; break;
                }
            });
        }
        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                this.BeginWatching?.Invoke(this, null);
                while (true)
                {
                    try
                    {
                        GetChannelEventArgs getChannel = new GetChannelEventArgs() { Sorter = c => c.LastUpdate.Ticks };
                        this.GetChannel?.Invoke(this, getChannel);
                        if (getChannel.Channel != null)
                        {
                            Log(this, $"Select channel #{getChannel.Channel.Id} for update");
                            GetItemsEventArgs published = new GetItemsEventArgs() { Channel = getChannel.Channel };
                            this.GetPublishedItems?.Invoke(this, published);
                            IEnumerable<Item> @new = published.Items.Except(getChannel.Channel.Items, Item.Comparer);
                            if (@new.Count() >= 0)
                            {
                                this.NewItemPublished?.Invoke(this, new NewItemPublishedEventArgs() { Items = @new });
                            }
                        }
                        else
                        {
                            Log(this, "No channel for update");
                        }
                    }
                    catch { }
                    Thread.Sleep(TimeSpan.FromSeconds(WatchInterval));
                }
            });
        }
    }
}
