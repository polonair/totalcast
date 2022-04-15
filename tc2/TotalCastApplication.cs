using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace tc2
{
    class TotalCastApplication
    {
        private ServiceContainer Services = new ServiceContainer();

        static void Main(string[] args)
        {
            new TotalCastApplication().Initialize(new Configuration("./config.json")).Run();
            Thread.Sleep(Timeout.Infinite);
        }
        public TotalCastApplication()
        {
            ThreadPool.SetMinThreads(8, 8);
        }
        public TotalCastApplication Initialize(Configuration config)
        {
            Assembly
                .GetAssembly(this.GetType())
                .GetTypes()
                .Where(t => typeof(IService).IsAssignableFrom(t) && config.Contains(t.Name)).ToList()
                .ForEach(t => this.AddService(t, config.GetConfiguration(t.Name)));
            return this;
        }
        public void Run() => Services.GetAll<IRunnable>().ForEach(r => r.Run());
        private void AddService(Type type, ServiceParam[] config)
        {
            object svc = Activator.CreateInstance(type);
            if (svc.Is<IService>())
            {
                Services.Add(svc);
            }
            if (svc.Is<IConfigurable>())
            {
                svc.As<IConfigurable>().Configure(config);
            }
            if (svc.Is<IWatcher>())
            {
                svc.As<IWatcher>().BeginWatching += WatcherBeginWatching;
                svc.As<IWatcher>().GetChannel += WatcherGetChannel;
                svc.As<IWatcher>().GetPublishedItems += WatcherGetPublishedItems;
                svc.As<IWatcher>().NewItemPublished += WatcherNewItemPublished;
            }
            if (svc.Is<ILoggable>())
            {
                svc.As<ILoggable>().Log += LoggableLog;
            }
            if (svc.Is<INetworkable>())
            {
                svc.As<INetworkable>().Get += NetworkableGet;
                svc.As<INetworkable>().Post += NetworkablePost;
                svc.As<INetworkable>().LongPoll += NetworkableLongPoll;

            }
            if (svc.Is<IUi>())
            {
                svc.As<IUi>().LinkGotten += UILinkGotten;
                svc.As<IUi>().Subscribe += UISubscribe;
                svc.As<IUi>().UnSubscribe += UIUnSubscribe;
            }
            if (svc.Is<IProvider>())
            {
                svc.As<IProvider>().CheckMime += ProviderCheckMime;
            }
        }
        private void NetworkableLongPoll(object sender, GetEventArgs e)
        {
            e.Result = Services.GetOne<INetworker>().LongPoll(e.Link, e.TimeOut);
        }
        private void ProviderCheckMime(object sender, CheckMimeEventArgs e)
        {
            Services.GetAll<IConverter>().ForEach(c => e.CanProcess |= c.CanConvertFrom(e.InputMime));
        }
        private void NetworkablePost(object sender, PostEventArgs e)
        {
            e.Result = Services.GetOne<INetworker>().Post(e.Link, e.Content);
        }
        private void UISubscribe(object sender, LinkInfoEventArgs e)
        {
            Channel channel = new Channel() { LastUpdate = DateTime.Now, Service = e.LinkInfo.Service, SourceId = e.LinkInfo.SourceId };
            IEnumerable<Item> items = Services.GetAll<IProvider>().Where(p => p.CanProcess(channel)).FirstOrDefault().GetPublishedItems(channel);
            channel.Items = new List<Item>(items);
            channel.Items.ForEach(i => i.State = ItemState.Pass);
            Services.GetOne<IDatabase>().Subscribe(channel);
        }
        private void UIUnSubscribe(object sender, LinkInfoEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void UILinkGotten(object sender, LinkInfoEventArgs e)
        {
            IProvider p = Services.GetAll<IProvider>().Where(p => p.CanProcess(e.Link)).FirstOrDefault();
            e.LinkInfo = p.GetLinkInfo(e.Link);
            if (e.LinkInfo.Type == LinkType.Channel)
            {
                e.LinkInfo.IsSubscribed = Services.GetOne<IDatabase>().IsSubscribed(e.LinkInfo.SourceId);
            }
        }
        private void NetworkableGet(object sender, GetEventArgs e)
        {
            e.Result = Services.GetOne<INetworker>().Get(e.Link);
        }
        private void WatcherNewItemPublished(object sender, NewItemPublishedEventArgs e)
        {
            IDatabase db = Services.GetOne<IDatabase>();
            Item item = e.Items.FirstOrDefault();
            if (item == null) return;
            LoggableLog(this, new() { Message = $"new item published '{item.Title}'" });
            db.BeginDownload(item);
            ThreadPool.QueueUserWorkItem(o =>
            {
                Content loaded = Services.GetAll<IProvider>().Where(p => p.CanProcess(item.Channel)).FirstOrDefault().LoadContent(item);
                if (loaded != null)
                {
                    Content converted = Services.GetAll<IConverter>().Where(c => c.CanConvertFrom(loaded.Mime)).FirstOrDefault().Convert(item, loaded);
                    Services.GetOne<ITagWriter>().WriteTags(item, converted);
                    Services.GetAll<IPublicator>().ForEach(p => p.Publish(item, converted));
                    db.DownloadDone(item);
                }
                else
                {
                    db.CannotDownload(item);
                }
            });
        }
        private void WatcherGetPublishedItems(object sender, GetItemsEventArgs e)
        {
            e.Items = Services.GetAll<IProvider>().Where(p => p.CanProcess(e.Channel)).FirstOrDefault().GetPublishedItems(e.Channel);
        }
        private void WatcherGetChannel(object sender, GetChannelEventArgs e)
        {
            e.Channel = Services.GetOne<IDatabase>()?.SelectChannel(e.Sorter);
        }
        private void LoggableLog(object sender, LogEventArgs e) => Services.GetAll<ILogger>().ForEach(l => l.Log(e.Message));
        private void WatcherBeginWatching(object sender, EventArgs e) => Services.GetOne<IDatabase>()?.CleanPending();
    }
}
