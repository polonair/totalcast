using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace tc2
{
    class TgResult<T>
    {
        public bool ok;
        public T result;
    }
    class User
    {
        public long id;
        public bool is_bot;
        public string first_name;
        public string last_name;
        public string username;
        public string language_code;
    }
    class Chat
    {
        public long id;
        public string first_name;
        public string last_name;
        public string username;
        public string type;
    }
    class Message
    {
        internal int message_id;
        public User from;
        public Chat chat;
        public int date;
        public string text;
    }
    class Update
    {
        internal int update_id;
        public Message message;

    }
    class Telegram : IService, IPublicator, IConfigurable, INetworkable, IUi, IRunnable
    {
        public string BotApiKey { get; private set; }
        public string ChatId { get; private set; }
        public int LongPollTimeout { get; private set; }

        public event EventHandler<GetEventArgs> Get;
        public event EventHandler<PostEventArgs> Post;
        public event EventHandler<LinkInfoEventArgs> LinkGotten;
        public event EventHandler<LinkInfoEventArgs> Subscribe;
        public event EventHandler<LinkInfoEventArgs> UnSubscribe;
        public event EventHandler<GetEventArgs> LongPoll;

        public void Configure(ServiceParam[] config)
        {
            config.ToList().ForEach(p =>
            {
                switch (p.key)
                {
                    case "botApiKey": this.BotApiKey = (string)p.value; break;
                    case "chatId": this.ChatId = (string)p.value; break;
                    case "longPollTimeout": this.LongPollTimeout = 600; break;
                }
            });
        }
        public void Publish(Item item, Content converted)
        {
            string text = $"TC: [{item.Title.TelegramMarkDownReplace()}]({ item.Link })\n\n" +
            $"*{item.Author.TelegramMarkDownReplace()}*\n\n" +
            $"{item.Published.ToString().TelegramMarkDownReplace()}\n\n" +
            $"{item.Description?.TelegramMarkDownReplace()}";

            PostEventArgs e = new PostEventArgs()
            {
                Link = $"https://api.telegram.org/bot{BotApiKey}/sendAudio",
                Content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))
                {
                    { new StreamContent(converted.Stream), "audio", $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.mp3" },
                    { new StringContent(ChatId), "chat_id" },
                    { new StringContent(text.TelegramMarkDownTrim(1000)), "caption" },
                    { new StringContent("MarkdownV2"), "parse_mode" },
                    { new StringContent(converted.Duration.ToString()), "duration" },
                    { new StringContent(item.Author), "performer" },
                    { new StringContent(item.Title), "title" }
                }
            };
            Post?.Invoke(this, e);
            string result = e.Result.ReadAsString();
        }
        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                int nextup = 0;
                this.DeleteWebhook();
                while (true)
                {
                    try
                    {
                        Update[] u = this.GetUpdate(nextup);
                        if (u != null)
                        {
                            foreach (Update up in u)
                            {
                                this.ProcessUpdate(up);
                                nextup = up.update_id + 1;
                            }
                        }
                    }
                    catch
                    {
                        Thread.Sleep(10000);
                    }
                }
            });
        }
        private void DeleteWebhook()
        {
            this.Get?.Invoke(this, new() { Link = $"https://api.telegram.org/bot{BotApiKey}/deleteWebhook?drop_pending_updates=true" });
        }
        private void ProcessUpdate(Update up)
        {
            throw new NotImplementedException();
        }
        private Update[] GetUpdate(int up)
        {
            var url = (up == 0)
                ? $"https://api.telegram.org/bot{BotApiKey}/getUpdates?timeout={LongPollTimeout}"
                : $"https://api.telegram.org/bot{BotApiKey}/getUpdates?timeout={LongPollTimeout}&offset={up}";
            GetEventArgs e = new() { Link = url, TimeOut = LongPollTimeout + 1 };
            this.LongPoll?.Invoke(this, e);
            if (e.Result != null)
            {
                string body = e.Result.ReadAsString();
                TgResult<Update[]> result = Newtonsoft.Json.JsonConvert.DeserializeObject<TgResult<Update[]>>(body);
                if (result != null && result.ok) return result.result;
            }
            return null;
        }
    }
}
