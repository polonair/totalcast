using Newtonsoft.Json;

namespace tc2
{
    [JsonObject(MemberSerialization.OptIn)]
    class VideoDetails
    {
        [JsonProperty("videoId")] public string Id;
        [JsonProperty("title")] public string Title;
        [JsonProperty("lengthSeconds")] public int Duration;
        [JsonProperty("channelId")] public string ChannelId;
        [JsonProperty("isOwnerViewing")] public bool IsOwnerViewing;
        [JsonProperty("shortDescription")] public string Description;
        [JsonProperty("isCrawlable")] public bool IsCrawlable;
        [JsonProperty("thumbnail")] public ThumbnailCollection Thumbnail;
        [JsonProperty("allowRatings")] public bool AllowRatings;
        [JsonProperty("viewCount")] public int ViewCount;
        [JsonProperty("author")] public string Author;
        [JsonProperty("isPrivate")] public bool IsPrivate;
        [JsonProperty("isUnpluggedCorpus")] public bool IsUnpluggedCorpus;
        [JsonProperty("isLiveContent")] public bool IsLiveContent;
    }
}
