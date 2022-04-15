using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tc2
{
    class LocalDB : IService, IDatabase, IConfigurable
    {
        class Context : DbContext
        {
            public DbSet<Channel> Channels => Set<Channel>();
            public DbSet<Item> Items => Set<Item>();
            public string Path { get; }

            public Context(string path)
            {
                this.Path = path;
                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite($"Data Source={Path}");
            }
        }
        public string DbFileName { get; private set; }
        public void BeginDownload(Item item)
        {
            using (Context db = new Context(this.DbFileName))
            {
                item.State = ItemState.Downloading;
                db.Attach(item.Channel);
                db.Items.Add(item);
                db.SaveChanges();
            }
        }
        public void CannotDownload(Item item)
        {
            using (Context db = new Context(this.DbFileName))
            {
                db.Attach(item);
                item.State = ItemState.CanNotDownload;
                db.SaveChanges();
            }
        }
        public void DownloadDone(Item item)
        {
            using (Context db = new Context(this.DbFileName))
            {
                db.Attach(item);
                item.State = ItemState.Saved;
                db.SaveChanges();
            }
        }
        public Channel SelectChannel(Func<Channel, long> sorter)
        {
            using (Context db = new Context(this.DbFileName))
            {
                Channel c = db.Channels.Include(c => c.Items).OrderBy(sorter).FirstOrDefault();
                if (c != null)
                {
                    c.LastUpdate = DateTime.Now;
                    db.SaveChanges();
                }
                return c;
            }
        }
        public IEnumerable<Item> SelectLoadedChannels(Channel channel, int maxItems)
        {
            throw new NotImplementedException();
        }
        public void Configure(ServiceParam[] config)
        {
            config.ToList().ForEach(p =>
            {
                switch (p.key)
                {
                    case "dbFileName": this.DbFileName = (string)p.value; break;
                }
            });
        }
        public void Subscribe(Channel channel)
        {
            using (Context db = new Context(this.DbFileName))
            {
                db.Channels.Add(channel);
                db.SaveChanges();
            }
        }
        public bool IsSubscribed(string sourceId)
        {
            using (Context db = new Context(this.DbFileName))
            {
                return db.Channels.Where(c => c.SourceId == sourceId).Count() > 0;
            }
        }
        public void CleanPending()
        {
            using (Context db = new Context(this.DbFileName))
            {
                db.Items.RemoveRange(db.Items.Where(i => (i.State == ItemState.CanNotDownload) || (i.State == ItemState.Downloading)));
                db.SaveChanges();
            }
        }
    }
}
