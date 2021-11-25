using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Id3.Frames;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace YTBotLoader
{
    class Updates
    {
        public bool ok { get; set; }
        public Update[] result { get; set; }
    }
    public class Message
    {
        public int message_id { get; set; }
        public User from { get; set; }
        public Chat sender_chat { get; set; }
        public int date { get; set; }
        public Chat chat { get; set; }
        public User forward_from { get; set; }
        public Chat forward_from_chat { get; set; }
        public int forward_from_message_id { get; set; }
        public string forward_signature { get; set; }
        public string forward_sender_name { get; set; }
        public int forward_date { get; set; }
        public Message reply_to_message { get; set; }
        public User via_bot { get; set; }
        public int edit_date { get; set; }
        public string media_group_id { get; set; }
        public string author_signature { get; set; }
        public string text { get; set; }
        public Message[] entities { get; set; }
        public Animation animation { get; set; }
        public Audio audio { get; set; }
        public Document document { get; set; }
        public PhotoSize[] photo { get; set; }
        public Sticker sticker { get; set; }
        public Video video { get; set; }
        public VideoNote video_note { get; set; }
        public Voice voice { get; set; }
        public string caption { get; set; }
        public Message[] caption_entities { get; set; }
        public Contact contact { get; set; }
        public Dice dice { get; set; }
        public Game game { get; set; }
        public Poll poll { get; set; }
        public Venue venue { get; set; }
        public Location location { get; set; }
        public User[] new_chat_members { get; set; }
        public User left_chat_member { get; set; }
        public string new_chat_title { get; set; }
        public PhotoSize[] new_chat_photo { get; set; }
        public bool delete_chat_photo { get; set; }
        public bool group_chat_created { get; set; }
        public bool supergroup_chat_created { get; set; }
        public bool channel_chat_created { get; set; }
        public Message message_auto_delete_timer_changed { get; set; }
        public int migrate_to_chat_id { get; set; }
        public int migrate_from_chat_id { get; set; }
        public Message pinned_message { get; set; }
        public Invoice invoice { get; set; }
        public SuccessfulPayment successful_payment { get; set; }
        public string connected_website { get; set; }
        public PassportData passport_data { get; set; }
        public ProximityAlertTriggered proximity_alert_triggered { get; set; }
        public VoiceChat voice_chat_started { get; set; }
        public VoiceChat voice_chat_ended { get; set; }
        public VoiceChat voice_chat_participants_invited { get; set; }
        public InlineKeyboardMarkup reply_markup { get; set; }
    }
    public class InlineKeyboardButton
    {
        public string text;
        public string callback_data;
    }
    public class InlineKeyboardMarkup 
    {
        public InlineKeyboardButton[][] inline_keyboard;
    }
    public class VoiceChat { }
    public class ProximityAlertTriggered { }
    public class PassportData { }
    public class SuccessfulPayment { }
    public class Invoice { }
    public class Location { }
    public class Venue { }
    public class Game { }
    public class Dice { }
    public class Contact { }
    public class Voice { }
    public class VideoNote { }
    public class Video { }
    public class Sticker { }
    public class PhotoSize { }
    public class Document { }
    public class Audio { }
    public class Animation { }
    class InlineQuery { }
    class ChosenInlineResult { }
    public class EditMessageText
    {
        public long chat_id;
        public int message_id;
        public string text;
        public InlineKeyboardMarkup reply_markup;
    }
    public class CallbackQuery
    {
        public string id;
        public User from;
        public Message message;
        public string inline_message_id;
        public string chat_instance;
        public string data;
        public string game_short_name;
    }
    public class ShippingQuery { }
    public class PreCheckoutQuery { }
    public class Poll { }
    public class PollAnswer { }
    public class ChatPhoto { }
    public class ChatPermissions { }
    public class ChatLocation { }
    public class Chat
    {
        public long id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public ChatPhoto photo { get; set; }
        public string bio { get; set; }
        public string description { get; set; }
        public string invite_link { get; set; }
        public Message pinned_message { get; set; }
        public ChatPermissions permissions { get; set; }
        public int slow_mode_delay { get; set; }
        public int message_auto_delete_time { get; set; }
        public string sticker_set_name { get; set; }
        public bool can_set_sticker_set { get; set; }
        public int linked_chat_id { get; set; }
        public ChatLocation location { get; set; }
    }
    public class User
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }
        public bool can_join_groups { get; set; }
        public bool can_read_all_group_messages { get; set; }
        public bool supports_inline_queries { get; set; }
    }
    class ChatMember
    {
        public User user { get; set; }
        public string status { get; set; }
        public string custom_title { get; set; }
        public bool is_anonymous { get; set; }
        public bool can_be_edited { get; set; }
        public bool can_manage_chat { get; set; }
        public bool can_post_messages { get; set; }
        public bool can_edit_messages { get; set; }
        public bool can_delete_messages { get; set; }
        public bool can_manage_voice_chats { get; set; }
        public bool can_restrict_members { get; set; }
        public bool can_promote_members { get; set; }
        public bool can_change_info { get; set; }
        public bool can_invite_users { get; set; }
        public bool can_pin_messages { get; set; }
        public bool is_member { get; set; }
        public bool can_send_messages { get; set; }
        public bool can_send_media_messages { get; set; }
        public bool can_send_polls { get; set; }
        public bool can_send_other_messages { get; set; }
        public bool can_add_web_page_previews { get; set; }
        public int until_date { get; set; }
    }
    class ChatInviteLink
    {
        public string invite_link { get; set; }
        public User creator { get; set; }
        public bool is_primary { get; set; }
        public bool is_revoked { get; set; }
        public int expire_date { get; set; }
        public int member_limit { get; set; }
    }
    class ChatMemberUpdated
    {
        public Chat chat { get; set; }
        public User from { get; set; }
        public int date { get; set; }
        public ChatMember old_chat_member { get; set; }
        public ChatMember new_chat_member { get; set; }
        public ChatInviteLink invite_link { get; set; }
    }
    class Update
    {
        public int update_id { get; set; }
        public Message message { get; set; }
        public Message edited_message { get; set; }
        public Message channel_post { get; set; }
        public Message edited_channel_post { get; set; }
        public InlineQuery inline_query { get; set; }
        public ChosenInlineResult chosen_inline_result { get; set; }
        public CallbackQuery callback_query { get; set; }
        public ShippingQuery shipping_query { get; set; }
        public PreCheckoutQuery pre_checkout_query { get; set; }
        public Poll poll { get; set; }
        public PollAnswer poll_answer { get; set; }
        public ChatMemberUpdated my_chat_member { get; set; }
        public ChatMemberUpdated chat_member { get; set; }
    }
    class YoutubeRequest
    {
        public class _thumbnail
        {
            public int height;
            public string resolution;
            public string url;
            public int width;
            public string id;
        }

        public string channel;
        public string id;
        public string webpage_url;
        public string fulltitle;
        public string uploader;
        public string description;
        public double duration;
        public _thumbnail[] thumbnails;
        public bool? is_live;
    }
    class SendMessageArgs
    {
        public long chat_id;
        public string text;
    }
    class SendMessageArgsReplyMarkup
    {
        public long chat_id;
        public string text;
        public InlineKeyboardMarkup reply_markup;
    }
    class BotService2 : ApplicationContext
    {
        static Random Random = new Random();
        DriveService dservice;

        const string BOTAPIKEY = "1641144444:AAEyvZI7fYrd3vC_adS-kXe-W2j6jej5F-E";

        private HttpClient Client;

        public BotService2()
        {
            this.Client = new HttpClient();
        }
        internal void Start()
        {
            /*var cred = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets {
                ClientId = "131303684751-n3dpbk2lu2mna4ra69kp45l2btphm96c.apps.googleusercontent.com", ClientSecret = "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"
            }, new[] { DriveService.ScopeConstants.Drive}, "user", CancellationToken.None, new FileDataStore("Drive.Auth.Store")).Result;

            dservice = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred
            });*/
            new Thread(new ThreadStart(() =>
            {
                int nextup = 0;
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
            })).Start();

            new Thread(new ThreadStart(() =>
            {
                int nextup = 0;
                while (true)
                {
                    try
                    {
                        UpdateFeed();
                    }
                    catch { }
                    finally
                    {
                        Thread.Sleep(Random.Next(1000, 60000));
                    }
                }
            })).Start();
        }

        private void UpdateFeed()
        {
            string[] list = Directory.GetFiles("./", "*.settings");
            foreach (string item in list)
            {
                Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText(item));
                if (s.subscriptions != null && s.subscriptions.Length > 0)
                {
                    int idx = 0;
                    for (int i = 0; i< s.subscriptions.Length; i++)
                    {
                        if (s.subscriptions[i].take_after < s.subscriptions[idx].take_after)
                            idx = i;
                    }
                    Settings._subscription sub = s.subscriptions[idx];
                    long ta = sub.take_after;
                    sub.take_after = DateTimeOffset.Now.ToUnixTimeSeconds();
                    Log($"[{DateTime.Now}]: Check for {sub.title}");
                    LoadNew(sub.id, ta, s.chat_id, sub);
                }
                string content = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                File.WriteAllText(item, content);
            }
        }

        private void Log(string v)
        {
            try { File.AppendAllLines("./log", new string[] { v }); }
            catch { }
        }

        private void LoadNew(string ch_id, long ta, long chat, Settings._subscription sub)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"https://www.youtube.com/feeds/videos.xml?channel_id={ch_id}");
            foreach (XmlNode n1 in doc.DocumentElement.SelectNodes("*"))
            {
                if (n1.Name == "entry")
                {
                    string link = null;
                    string vid = null;
                    int views = -1;
                    foreach (XmlNode n2 in n1.SelectNodes("*"))
                    {
                        if (n2.Name == "link")
                        {
                            link = n2.Attributes["href"].Value;
                        }
                        else if (n2.Name == "yt:videoId")
                        {
                            vid = n2.InnerText;
                        }
                        else if (n2.Name == "media:group")
                        {
                            foreach (XmlNode n3 in n2.SelectNodes("*"))
                            {
                                if (n3.Name == "media:community")
                                {
                                    foreach (XmlNode n4 in n3.SelectNodes("*"))
                                    {
                                        if (n4.Name == "media:statistics")
                                        {
                                            views = int.Parse(n4.Attributes["views"].Value);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!sub.watched.Contains(vid) && views > 0)
                    {
                        using (Process p = Process.Start(new ProcessStartInfo()
                        {
                            FileName = "youtube-dl.exe",
                            Arguments = $"{link} -j",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }))
                        {
                            string result = p.StandardOutput.ReadToEnd();
                            YoutubeRequest yr = Newtonsoft.Json.JsonConvert.DeserializeObject<YoutubeRequest>(result);
                            if (yr != null && yr.is_live == null)
                            {
                                Array.Resize(ref sub.watched, sub.watched.Length + 1);
                                sub.watched[sub.watched.Length - 1] = vid;
                                Post(link, chat);
                                return;
                            }
                        }
                    }
                }
            }
            /*foreach (XmlNode n1 in doc.DocumentElement.SelectNodes("*"))
            {
                if (n1.Name == "entry")
                {
                    string link = "";
                    DateTime date= DateTime.Now;
                    foreach (XmlNode n2 in n1.SelectNodes("*"))
                    {
                        if (n2.Name == "link")
                        {
                            link = n2.Attributes["href"].Value;
                        }
                        else if (n2.Name == "published")
                        {
                            date = DateTime.Parse(n2.InnerText);
                        }
                    }
                    if (((DateTimeOffset)date).ToUnixTimeSeconds() > ta)
                    {
                        Post(link, chat);
                    }
                }
            }*/
        }

        private Update[] GetUpdate(int up = 0)
        {
            string allowed = "&allowed_updates=%5B%22message%22%2C%22edited_message%22%2C%22channel_post%22%2C%22edited_channel_post%22%2C%22inline_query%22%2C%22chosen_inline_result%22%2C%22callback_query%22%2C%22shipping_query%22%2C%22pre_checkout_query%22%2C%22poll%22%2C%22poll_answer%22%2C%22my_chat_member%22%2C%22chat_member%22%2C%22chat_join_request%22%5D";
            var url = (up == 0)
                ? $"https://api.telegram.org/bot{BOTAPIKEY}/getUpdates?timeout=600{allowed}"
                : $"https://api.telegram.org/bot{BOTAPIKEY}/getUpdates?timeout=600&offset={up}{allowed}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (HttpClient ServClient = new HttpClient())
            {
                ServClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                using (var response = ServClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result)
                {
                    string body = response.Content.ReadAsStringAsync().Result;
                    Updates ups = Newtonsoft.Json.JsonConvert.DeserializeObject<Updates>(body);
                    if (ups != null && ups.ok) return ups.result;
                }
            }
            return null;
        }
        private void ProcessUpdate(Update u)
        {
            if (u.message != null)
            {
                if (!string.IsNullOrEmpty(u.message.text))
                {
                    string link = u.message.text.Trim();
                    if (link == "/start") this.StartCommand(u);
                    else if (link.StartsWith("https://")) this.LinkAction(u);
                    else if (link == "/listsubs") this.ListSubscriptionsCommand(u);
                    //else if (link.StartsWith("/subs")) this.SubscribeCommand(u);
                    //else if (link.StartsWith("/load")) this.LoadCommand(u);
                }
            }
            else if (u.callback_query != null)
            {
                if (!string.IsNullOrEmpty(u.callback_query.data))
                {
                    string data = u.callback_query.data.Trim();
                    if (data.StartsWith("#select")) this.SelectQuery(u);
                    else if (data.StartsWith("#load")) this.LoadQuery(u);
                    else if (data.StartsWith("#subscribe")) this.SubQuery(u);
                    else if (data.StartsWith("#info")) this.InfoQuery(u);
                    else if (data.StartsWith("#unsub")) this.USubQuery(u);
                    else if (data.StartsWith("#cancel")) this.CancelQuery(u);
                }
            }
        }

        private void CancelQuery(Update u)
        {
            long id = u.callback_query.message.chat.id;
            DeleteMessage(u.callback_query.message.chat.id, u.callback_query.message.message_id);
        }

        private void USubQuery(Update u)
        {
            long id = u.callback_query.message.chat.id;
            string link = u.callback_query.data.Trim().Substring(6).Trim();
            link = link.Substring(1, link.Length - 2);
            if (File.Exists($"{id}.settings"))
            {
                Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{id}.settings"));
                List<Settings._subscription> newsubs = new List<Settings._subscription>();
                bool isUnsub = false;
                string title = null;
                foreach(Settings._subscription sub in s.subscriptions)
                {
                    if (sub.id != link) newsubs.Add(sub);
                    else { title = sub.title; isUnsub = true; }
                }
                if (isUnsub)
                {
                    s.subscriptions = newsubs.ToArray();
                    string content = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                    File.WriteAllText($"{id}.settings", content);
                    EditMessage(new EditMessageText()
                    {
                        chat_id = u.callback_query.message.chat.id,
                        message_id = u.callback_query.message.message_id,
                        text = $"You're now unsubscribed from channel '{title}'",
                        reply_markup = new InlineKeyboardMarkup() { inline_keyboard = new InlineKeyboardButton[0][] },
                    });
                    //SendMessage($"You're now unsubscribed from channel '{title}'", id);
                }
                else
                {
                    EditMessage(new EditMessageText()
                    {
                        chat_id = u.callback_query.message.chat.id,
                        message_id = u.callback_query.message.message_id,
                        text = $"You've never been subscribed to '{link}'",
                        reply_markup = new InlineKeyboardMarkup() { inline_keyboard = new InlineKeyboardButton[0][] },
                    });
                    //SendMessage($"You've never been subscribed to '{link}'", id);
                }
            }
        }
        private void InfoQuery(Update u) { }
        private void SubQuery(Update u) 
        {

            long id = u.callback_query.message.chat.id;
            string ch_id = u.callback_query.data.Trim().Substring(10).Trim();
            ch_id = ch_id.Substring(1, ch_id.Length - 2);

            if (File.Exists($"{id}.settings"))
            {
                Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{id}.settings"));
                Settings._subscription sub = CreateSubscription(ch_id);
                if (s.subscriptions == null) s.subscriptions = new Settings._subscription[] { sub };
                else
                {
                    Array.Resize(ref s.subscriptions, s.subscriptions.Length + 1);
                    s.subscriptions[s.subscriptions.Length - 1] = sub;
                }
                string content = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                File.WriteAllText($"{id}.settings", content);
                EditMessage(new EditMessageText() 
                {
                     chat_id = u.callback_query.message.chat.id,
                     message_id = u.callback_query.message.message_id,
                     text = $"You are now subscribed on '{sub.title}'",
                     reply_markup = new InlineKeyboardMarkup() { inline_keyboard = new InlineKeyboardButton[0][] },
                });
                //SendMessage($"You are now subscribed on '{sub.title}'", id);
            }
        }
        private void LoadQuery(Update u) 
        { 
            long id = u.callback_query.message.chat.id;
            string link = u.callback_query.data.Trim().Substring(5).Trim();
            link = link.Substring(1, link.Length - 2);
            EditMessageText e = new EditMessageText()
            {
                chat_id = u.callback_query.message.chat.id,
                message_id = u.callback_query.message.message_id,
                text = "Loading video, please wait...",
                reply_markup = new InlineKeyboardMarkup()
                {
                    inline_keyboard = new InlineKeyboardButton[0][],
                },
            };
            EditMessage(e);
            //SendMessage("Loading video, please wait...", id);
            Post(link, id);
            DeleteMessage(e.chat_id, e.message_id);
        }

        private void DeleteMessage(long chat_id, int message_id)
        {
            using (var message = Client.GetAsync($"https://api.telegram.org/bot{BOTAPIKEY}/deleteMessage?chat_id={chat_id}&message_id={message_id}").Result)
            {
                var input = message.Content.ReadAsStringAsync().Result;
            }
        }

        private void EditMessage(EditMessageText e)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(e);
            HttpContent content = new StringContent(data, Encoding.UTF8);
            content.Headers.ContentType.MediaType = "application/json";
            using (var message = Client.PostAsync($"https://api.telegram.org/bot{BOTAPIKEY}/editMessageText", content).Result)
            {
                var input = message.Content.ReadAsStringAsync().Result;
            }
        }

        private void LinkAction(Update u)
        {
            long id = u.message.chat.id;
            string link = u.message.text.Trim();
            string page = Client.GetAsync(link).Result.Content.ReadAsStringAsync().Result;
            int vidIdx = page.IndexOf("<meta itemprop=\"videoId\" content=");
            int chIdx = page.IndexOf("<meta itemprop=\"channelId\" content=");

            Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{id}.settings"));

            if (vidIdx > 0)
            {
                string s1 = page.Substring(chIdx + 36);
                string chid = s1.Substring(0, s1.IndexOf("\">"));
                s1 = page.Substring(vidIdx + 34);
                string vid = s1.Substring(0, s1.IndexOf("\">"));

                SendMessageArgsReplyMarkup sm = new SendMessageArgsReplyMarkup()
                {
                    chat_id = id,
                    text = $"Link to video found",
                    reply_markup = new InlineKeyboardMarkup()
                    {
                        inline_keyboard = new InlineKeyboardButton[][]
                        {
                            new InlineKeyboardButton[]
                            {
                                new InlineKeyboardButton()
                                {
                                    text = $"Download",
                                    callback_data=$"#load [{link}]",
                                },
                                new InlineKeyboardButton()
                                {
                                    text = $"Cancel",
                                    callback_data=$"#cancel",
                                },
                            }
                        }
                    },
                };
                SendMessage(sm, id);
            }
            else if (chIdx > 0) 
            {
                string s1 = page.Substring(chIdx + 36);
                string chid = s1.Substring(0, s1.IndexOf("\">"));
                string chName = GetChannelTitle(chid);

                bool subscribed = false;
                foreach(Settings._subscription sub in s.subscriptions)
                {
                    if (sub.id == chid)
                    {
                        subscribed = true;
                        break;
                    }
                }

                SendMessageArgsReplyMarkup sm = new SendMessageArgsReplyMarkup()
                {
                    chat_id = id,
                    text = $"Link to channel \"{chName}\" found, you're {(subscribed?"subscribed":"not subscribed")}, so...",
                    reply_markup = new InlineKeyboardMarkup()
                    {
                        inline_keyboard = new InlineKeyboardButton[][]
                        {
                            new InlineKeyboardButton[]
                            {
                                subscribed
                                ?new InlineKeyboardButton()
                                {
                                    text = $"Unsubscribe",
                                    callback_data=$"#unsub [{chid}]",
                                }
                                :new InlineKeyboardButton()
                                {
                                    text = $"Subscribe",
                                    callback_data=$"#subscribe [{chid}]",
                                },
                                new InlineKeyboardButton()
                                {
                                    text = $"Cancel",
                                    callback_data=$"#cancel",
                                },
                            }
                        }
                    },
                };
                SendMessage(sm, id);
            }
            else 
            { 
                SendMessage("Can not figure out this link", id);
            }
        }
        private void SelectQuery(Update u)
        {
            long id = u.callback_query.message.chat.id;
            string ch_id = u.callback_query.data.Trim().Substring(7).Trim();
            ch_id = ch_id.Substring(1, ch_id.Length - 2);
            if (File.Exists($"{id}.settings"))
            {
                Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{id}.settings"));
                foreach (Settings._subscription sub in s.subscriptions)
                {
                    if (sub.id == ch_id)
                    {
                        EditMessageText em = new EditMessageText()
                        {
                            chat_id = id,
                            text = $"Channel \"{sub.title}\"",
                            message_id = u.callback_query.message.message_id,
                            reply_markup = new InlineKeyboardMarkup()
                            {
                                inline_keyboard = new InlineKeyboardButton[][]
                                {
                                    new InlineKeyboardButton[]
                                    {
                                        new InlineKeyboardButton()
                                        {
                                            text = $"Info",
                                            callback_data=$"#info [{sub.id}]",
                                        },
                                        new InlineKeyboardButton()
                                        {
                                            text = $"Unsubscribe",
                                            callback_data=$"#unsub [{sub.id}]",
                                        },
                                        new InlineKeyboardButton()
                                        {
                                            text = $"Cancel",
                                            callback_data=$"#cancel",
                                        },
                                    }
                                }
                            },
                        };
                        SendMessageArgsReplyMarkup sm = new SendMessageArgsReplyMarkup()
                        {
                            chat_id = id,
                            text = $"Channel \"{sub.title}\"",
                            reply_markup = new InlineKeyboardMarkup()
                            {
                                inline_keyboard = new InlineKeyboardButton[][]
                                {
                                    new InlineKeyboardButton[]
                                    {
                                        new InlineKeyboardButton()
                                        {
                                            text = $"Info",
                                            callback_data=$"#info [{sub.id}]",
                                        },
                                        new InlineKeyboardButton()
                                        {
                                            text = $"Unsubscribe",
                                            callback_data=$"#unsub [{sub.id}]",
                                        },
                                    }
                                }
                            },
                        };
                        //SendMessage(sm, id);
                        EditMessage(em);
                        return;
                    }
                }
            }
        }
        private void LoadCommand(Update u)
        {
            long id = u.message.chat.id;
            string link = u.message.text.Trim().Substring(5).Trim();
            Post(link, id);
        }

        private void SubscribeCommand(Update u)
        {
            long id = u.message.chat.id;
            string ch_id = u.message.text.Trim().Substring(5).Trim();
            if (File.Exists($"{id}.settings"))
            {
                Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{id}.settings"));
                Settings._subscription sub = CreateSubscription(ch_id);
                /*string title = GetChannelTitle(ch_id);
                string[] vids = 
                Settings._subscription sub = new Settings._subscription() 
                {
                    id = ch_id,
                    title = title,
                    take_after = DateTimeOffset.Now.ToUnixTimeSeconds(),
                };*/
                if (s.subscriptions == null) s.subscriptions = new Settings._subscription[] { sub };
                else
                {
                    Array.Resize(ref s.subscriptions, s.subscriptions.Length + 1);
                    s.subscriptions[s.subscriptions.Length - 1] = sub;
                }
                string content = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                File.WriteAllText($"{id}.settings", content);
                SendMessage($"You are now subscribed on '{sub.title}'", id);
            }
        }

        private Settings._subscription CreateSubscription(string ch_id)
        {
            Settings._subscription result = new Settings._subscription();
            result.id = ch_id;
            result.title = null;
            XmlDocument doc = new XmlDocument();
            doc.Load($"https://www.youtube.com/feeds/videos.xml?channel_id={ch_id}");
            foreach(XmlNode n1 in doc.DocumentElement.SelectNodes("*"))
            {
                if (n1.Name == "author")
                {
                    foreach (XmlNode n2 in n1.SelectNodes("*"))
                    {
                        if (n2.Name == "name")
                        {
                            result.title = n2.InnerText;
                        }
                        if (!string.IsNullOrEmpty(result.title)) break;
                    }
                }
                if (!string.IsNullOrEmpty(result.title)) break;
            }

            List<string> ids = new List<string>();

            foreach(XmlNode n1 in doc.DocumentElement.SelectNodes("*"))
            {
                if (n1.Name == "entry")
                {
                    string id = null;
                    foreach (XmlNode n2 in n1.SelectNodes("*"))
                    {
                        if (n2.Name == "yt:videoId")
                        {
                            id = n2.InnerText;
                        }
                        if (!string.IsNullOrEmpty(id)) break;
                    }
                    ids.Add(id);
                }
            }
            result.watched = ids.ToArray();

            return result;
        }

        private string GetChannelTitle(string ch_id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"https://www.youtube.com/feeds/videos.xml?channel_id={ch_id}");
            foreach(XmlNode n1 in doc.DocumentElement.SelectNodes("*"))
            {
                if (n1.Name == "author")
                {
                    foreach (XmlNode n2 in n1.SelectNodes("*"))
                    {
                        if (n2.Name == "name")
                        {
                            return n2.InnerText;
                        }
                    }
                }
            }
            return null;
        }

        private void ListSubscriptionsCommand(Update u)
        {
            long id = u.message.chat.id;
            if (File.Exists($"{id}.settings"))
            {
                Settings s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{id}.settings"));
                string answer = "";
                if (s.subscriptions != null && s.subscriptions.Length > 0)
                {
                    List<InlineKeyboardButton[]> menu = new List<InlineKeyboardButton[]>();
                    foreach (Settings._subscription sub in s.subscriptions)
                    {
                        menu.Add(new InlineKeyboardButton[]
                        {
                            new InlineKeyboardButton()
                            {
                                text = $"\"{sub.title}\"",
                                callback_data=$"#select [{sub.id}]",
                            }
                        });
                    }
                    menu.Add(new InlineKeyboardButton[]
                    {
                        new InlineKeyboardButton()
                        {
                            text = $"Cancel",
                            callback_data=$"#cancel",
                        }
                    });
                    SendMessageArgsReplyMarkup sm = new SendMessageArgsReplyMarkup()
                    {
                        chat_id = id,
                        text = "You are subscribed on:",
                        reply_markup = new InlineKeyboardMarkup() 
                        {
                             inline_keyboard =  menu.ToArray() 
                        },
                    };
                    SendMessage(sm, id);
                }
                else
                {
                    SendMessage("You have no subscriptions", id);
                }
            }
        }
        private void SendMessage(string v, long id)
        {
            SendMessageArgs s = new SendMessageArgs()
            {
                chat_id = id,
                text = v
            };
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            HttpContent content = new StringContent(data, Encoding.UTF8);
            content.Headers.ContentType.MediaType = "application/json";
            using (var message = Client.PostAsync($"https://api.telegram.org/bot{BOTAPIKEY}/sendMessage", content).Result)
            {
                var input = message.Content.ReadAsStringAsync().Result;
            }
        }
        private void SendMessage(SendMessageArgsReplyMarkup s, long id)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            HttpContent content = new StringContent(data, Encoding.UTF8);
            content.Headers.ContentType.MediaType = "application/json";
            using (var message = Client.PostAsync($"https://api.telegram.org/bot{BOTAPIKEY}/sendMessage", content).Result)
            {
                var input = message.Content.ReadAsStringAsync().Result;
            }
        }
        private void StartCommand(Update u)
        {
            long id = u.message.chat.id;
            if (!File.Exists($"{id}.settings"))
            {
                Settings s = new Settings() { chat_id = id };
                string content = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                File.WriteAllText($"{id}.settings", content);
            }
        }
        private void Post(string link, long chat_id)
        {
            using (Process p = Process.Start(new ProcessStartInfo()
            {
                FileName = "youtube-dl.exe",
                Arguments = $"{link} -j",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }))
            {
                string result = p.StandardOutput.ReadToEnd();
                object r = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                YoutubeRequest yr = Newtonsoft.Json.JsonConvert.DeserializeObject<YoutubeRequest>(result);
                if (yr == null) return;
                /*ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "youtube-dl.exe",
                    Arguments = $"{link} -j",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                if (yr.duration > 9000)
                    psi.Arguments = $"-o %(id)s.%(ext)s -x --audio-format mp3 --audio-quality 9 --prefer-ffmpeg --ffmpeg-location ./ {link}";
                else if (yr.duration > 7200)
                    psi.Arguments = $"-o %(id)s.%(ext)s -x --audio-format mp3 --audio-quality 8 --prefer-ffmpeg --ffmpeg-location ./ {link}";
                else if (yr.duration > 5400)
                    psi.Arguments = $"-o %(id)s.%(ext)s -x --audio-format mp3 --audio-quality 7 --prefer-ffmpeg --ffmpeg-location ./ {link}";
                else if (yr.duration > 3600)
                    psi.Arguments = $"-o %(id)s.%(ext)s -x --audio-format mp3 --audio-quality 6 --prefer-ffmpeg --ffmpeg-location ./ {link}";
                else*/
                    //psi.Arguments = $"-o %(id)s.%(ext)s -x --audio-format mp3 --audio-quality 9 --prefer-ffmpeg --ffmpeg-location ./ {link}";
                // ffmpeg.exe -i Wr07Jf5Glds.m4a -codec:a libmp3lame -b:a 86k -ac 1 -abr:a 1 out.mp3
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "youtube-dl.exe",
                    Arguments = $"-o %(id)s.%(ext)s -x --audio-format mp3 --audio-quality 0 --prefer-ffmpeg --ffmpeg-location ./ {link}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }).WaitForExit();

                string sr = "";
                int bitrate = 8 * 45 * 1024 * 1024 / (int)yr.duration / 1000;
                if (bitrate > 256) bitrate = 256;
                if (bitrate < 32) sr = "-ar 22050";

                Process.Start(new ProcessStartInfo()
                {
                    FileName = "ffmpeg.exe",
                    Arguments = $"-i {yr.id}.mp3 -codec:a libmp3lame -b:a {bitrate}k {sr} -ac 1 -abr:a 1 done-{yr.id}.mp3",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }).WaitForExit();
                
                using (var mp3 = new Id3.Mp3($"done-{yr.id}.mp3", Id3.Mp3Permissions.ReadWrite))
                {
                    Id3.Id3Tag tag = mp3.GetTag(Id3.Id3TagFamily.Version2X);
                    if (tag == null) tag = new Id3.Id3Tag();
                    tag.Clear();
                    tag.Title = new TitleFrame() { Value = yr.fulltitle, EncodingType = Id3.Id3TextEncoding.Unicode };
                    tag.Artists = new ArtistsFrame() { EncodingType = Id3.Id3TextEncoding.Unicode };
                    tag.Artists.Value.Add(yr.uploader);
                    tag.Album = new AlbumFrame() { Value = "Polifm", EncodingType = Id3.Id3TextEncoding.Unicode, };
                    tag.Genre = new GenreFrame() { Value = "Polifm", EncodingType = Id3.Id3TextEncoding.Unicode, };
                    mp3.WriteTag(tag, Id3.Id3Version.V1X, Id3.WriteConflictAction.Replace);
                    mp3.WriteTag(tag, Id3.Id3Version.V23, Id3.WriteConflictAction.Replace);
                }

                string post = $"[{prep(yr.fulltitle)}]({yr.webpage_url})\n\n" +
                    $"*{prep(yr.uploader)}*\n\n" +
                    $"{prep(yr.description)}";

                if (post.Length > 1023)
                {
                    if (post[999] == '\\')
                        post = post.Substring(0, 999) + "\\.\\.\\.";
                    else
                        post = post.Substring(0, 1000) + "\\.\\.\\.";
                }

                using (var contentt = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    contentt.Add(new StreamContent(new MemoryStream(File.ReadAllBytes($"done-{yr.id}.mp3"))), "audio", $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.mp3");
                    contentt.Add(new StringContent(chat_id.ToString()), "chat_id");
                    contentt.Add(new StringContent(post), "caption");
                    contentt.Add(new StringContent("MarkdownV2"), "parse_mode");
                    contentt.Add(new StringContent(yr.duration.ToString()), "duration");
                    contentt.Add(new StringContent(yr.channel), "performer");
                    contentt.Add(new StringContent(yr.fulltitle), "title");
                    YoutubeRequest._thumbnail thumb = null;
                    foreach (YoutubeRequest._thumbnail t in yr.thumbnails)
                    {
                        if (t.height < 320 && t.width < 320)
                        {
                            if (thumb == null)
                            {
                                thumb = t;
                            }
                            else
                            {
                                if (t.width > thumb.width) thumb = t;
                            }
                        }
                    }
                    contentt.Add(new StringContent(thumb.url), "thumb");
                    using (var message = Client.PostAsync($"https://api.telegram.org/bot{BOTAPIKEY}/sendAudio", contentt).Result)
                    {
                        var input = message.Content.ReadAsStringAsync().Result;
                    }
                }
                /* //1GkoqyUI_hIRq1wvtAlMQ-tksSEtCtEjt
                 var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                 {
                     Name = $"{yr.id}.mp3",
                     Parents = new List<string>() { "1GkoqyUI_hIRq1wvtAlMQ-tksSEtCtEjt" }
                 };

                 string uploadedFileId;
                 // Create a new file on Google Drive
                 using (var fsSource = new FileStream($"done-{yr.id}.mp3", FileMode.Open, FileAccess.Read))
                 {
                     // Create a new file, with metadata and stream.
                     var request = dservice.Files.Create(fileMetadata, fsSource, "audio/mpeg");
                     request.Fields = "*";
                     var results = request.UploadAsync(CancellationToken.None).Result;

                     if (results.Status == UploadStatus.Failed)
                     {
                         Console.WriteLine($"Error uploading file: {results.Exception.Message}");
                     }

                     // the file id of the new file we created
                     uploadedFileId = request.ResponseBody?.Id;
                 }*/

                File.Copy($"done-{yr.id}.mp3", $"G:/Мой диск/Yt/{DateTimeOffset.Now.ToUnixTimeSeconds()}.mp3");

                File.Delete($"{yr.id}.mp3");
                File.Delete($"done-{yr.id}.mp3");

                return;
            }
        }
        string prep(string str)
        {
            return str
                .Replace("+", "\\+")
                .Replace("|", "\\|")
                .Replace("#", "\\#")
                .Replace("=", "\\=")
                .Replace("!", "\\!")
                .Replace("_", "\\_")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace(".", "\\.")
                .Replace("-", "\\-");
        }
    }
    class Settings 
    {
        public class _subscription
        {
            public string id;
            public string title;
            public long take_after;
            public string[] watched;
        }
        public _subscription[] subscriptions;
        public long chat_id;
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {

            //BotService bs = new BotService();
            BotService2 bs = new BotService2();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bs.Start();
            Application.Run(bs);
        }
    }
}