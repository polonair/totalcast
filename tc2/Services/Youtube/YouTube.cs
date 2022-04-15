using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace tc2
{
    class YouTube : IService, IProvider, INetworkable
    {
        public event EventHandler<GetEventArgs> Get;
        public event EventHandler<PostEventArgs> Post;
        public event EventHandler<CheckMimeEventArgs> CheckMime;
        public event EventHandler<GetEventArgs> LongPoll;

        public bool CanProcess(Channel channel) => channel.Service == "YOUTUBE";
        public bool CanProcess(string link) => link.StartsWith("https://www.youtube.com/");
        public LinkInfo GetLinkInfo(string link)
        {
            if (CanProcess(link))
            {
                GetEventArgs get = new GetEventArgs() { Link = link };
                this.Get?.Invoke(this, get);
                string content = get.Result.ReadAsString();
                if (content.Contains("var ytInitialPlayerResponse ="))
                {
                    string marker = "var ytInitialPlayerResponse = ";
                    content = content.Substring(content.IndexOf(marker) + marker.Length);
                    YoutubeVideo video = JsonConvert.DeserializeObject<YoutubeVideo>(content, new JsonSerializerSettings() { CheckAdditionalContent = false });
                    return new LinkInfo() { Type = LinkType.Item, Title = video.Details.Title };
                }
                else if (content.Contains("var ytInitialData ="))
                {
                    string marker = "var ytInitialData = ";
                    content = content.Substring(content.IndexOf(marker) + marker.Length);
                    YoutubeChannel video = JsonConvert.DeserializeObject<YoutubeChannel>(content, new JsonSerializerSettings() { CheckAdditionalContent = false });
                    return new LinkInfo() { Type = LinkType.Channel, Title = video.metadata.channelMetadataRenderer.title, Service = "YOUTUBE", SourceId = video.metadata.channelMetadataRenderer.externalId };
                }
            }
            return null;
        }
        public IEnumerable<Item> GetPublishedItems(Channel channel)
        {
            List<Item> result = new List<Item>();
            string link = $"https://www.youtube.com/feeds/videos.xml?channel_id={channel.SourceId}";
            GetEventArgs get = new GetEventArgs() { Link = link };
            this.Get?.Invoke(this, get);
            string content = get.Result.ReadAsString();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            XmlNamespaceManager xnsm = new XmlNamespaceManager(doc.NameTable);
            xnsm.AddNamespace("atom", "http://www.w3.org/2005/Atom");
            xnsm.AddNamespace("media", "http://search.yahoo.com/mrss/");
            xnsm.AddNamespace("yt", "http://www.youtube.com/xml/schemas/2015");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/atom:feed/atom:entry", xnsm);
            foreach (XmlNode node in nodes)
            {
                string id = node.SelectSingleNode("yt:videoId", xnsm).InnerText;
                string author = node.SelectSingleNode("atom:author/atom:name", xnsm).InnerText;
                string title = node.SelectSingleNode("atom:title", xnsm).InnerText;
                string pub = node.SelectSingleNode("atom:published", xnsm).InnerText;
                string description = node.SelectSingleNode("media:group/media:description", xnsm).InnerText;
                string l = node.SelectSingleNode("atom:link", xnsm).Attributes["href"].InnerText;
                result.Add(new Item() { SourceId = id, Author = author, Title = title, Description = description, Link = l, Published = DateTime.Parse(pub), Channel = channel }); ;
            }
            return result;
        }
        public Content LoadContent(Item item)
        {
            string content = "";
            string link = $"https://www.youtube.com/watch?v={item.SourceId}";
            GetEventArgs get = new GetEventArgs() { Link = link };
            this.Get?.Invoke(this, get);
            content = get.Result.ReadAsString();
            string marker = "var ytInitialPlayerResponse = ";
            if (content.IndexOf(marker) < 0) return null;
            content = content.Substring(content.IndexOf(marker) + marker.Length);
            YoutubeVideo video = JsonConvert.DeserializeObject<YoutubeVideo>(content, new JsonSerializerSettings() { CheckAdditionalContent = false });
            item.Title = video.Details.Title;
            item.Author = video.Details.Author;
            item.Description = video.Details.Description;
            item.Link = link;
            if (video.StreamingData == null) return null;
            video.StreamingData.Formats = video.StreamingData.Formats ?? Array.Empty<Format>();
            video.StreamingData.AdaptiveFormats = video.StreamingData.AdaptiveFormats ?? Array.Empty<Format>();
            if (video.StreamingData != null && video.Details.Duration > 0)
            {
                List<Format> itags = video.StreamingData.AdaptiveFormats.Union(video.StreamingData.Formats).OrderBy(f => f.ContentLength).ToList();
                foreach (Format itag in itags)
                {
                    try
                    {
                        if (itag.Url == null || itag.ContentLength == 0) continue;
                        CheckMimeEventArgs checkMime = new CheckMimeEventArgs() { InputMime = itag.MimeType };
                        CheckMime?.Invoke(this, checkMime);
                        if (!checkMime.CanProcess) continue;
                        get.Link = itag.Url;
                        this.Get?.Invoke(this, get);
                        Stream stream = get.Result.ReadAsStream();
                        if (stream == null || stream.Length == 0) continue;
                        return new Content() { Mime = itag.MimeType, Stream = stream, SampleRate = itag.AudioSampleRate, Channels = itag.AudioChannels, Duration = video.Details.Duration };
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
            return null;
        }
    }
}
