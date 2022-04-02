using System;
using System.IO;
using System.Threading;

namespace tc2
{
    enum LinkType {
        Item,
        Channel
    }
    class ConsoleUI : IService, IUi, IRunnable
    {
        private TextReader @in;
        private TextWriter @out;
        private TextWriter error;

        public event EventHandler<LinkInfoEventArgs> LinkGotten;
        public event EventHandler<LinkInfoEventArgs> Subscribe;
        public event EventHandler<LinkInfoEventArgs> UnSubscribe;

        public ConsoleUI() : this(Console.In, Console.Out, Console.Error) { }
        public ConsoleUI(TextReader @in, TextWriter @out, TextWriter error)
        {
            this.@in = @in;
            this.@out = @out;
            this.error = error;
        }
        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                @out.WriteLine("Start tc:");
                while (true)
                {
                    @out.Write("> ");
                    string line = @in.ReadLine();
                    this.Process(line);
                }
            });
        }
        private void Process(string line)
        {
            if (line.Trim().StartsWith("https://")) ProcessLink(line.Trim());
        }
        private void ProcessLink(string v)
        {
            LinkInfoEventArgs e = new LinkInfoEventArgs() { Link = v };
            this.LinkGotten?.Invoke(this, e);
            if (e.LinkInfo != null)
            {
                if (e.LinkInfo.Type == LinkType.Item) {
                    @out.WriteLine($"Found link to item, not supprted now");
                }
                else if (e.LinkInfo.Type == LinkType.Channel)
                {
                    if (e.LinkInfo.IsSubscribed) 
                    { 
                        @out.WriteLine($"Found link to channel {e.LinkInfo.Title}\nYou're already subscribed, unsubscribe? (y/N)");
                        string k = @in.ReadLine();
                        if (k == "y" || k == "Y") 
                        {
                            this.UnSubscribe?.Invoke(this, e);
                        }
                    }
                    else
                    {
                        @out.WriteLine($"Found link to channel {e.LinkInfo.Title}\nSubscribe? (y/N)");
                        string k = @in.ReadLine();
                        if (k == "y" || k == "Y") 
                        {
                            this.Subscribe?.Invoke(this, e);
                        }
                    }
                }
            }
            else
            {
                @out.WriteLine($"Cannot parse link {v}");
            }
        }
    }
}
