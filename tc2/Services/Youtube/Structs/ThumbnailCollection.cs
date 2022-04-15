using Newtonsoft.Json;

namespace tc2
{
    [JsonObject(MemberSerialization.OptIn)]
    class ThumbnailCollection
    {
        [JsonProperty("thumbnails")] public Thumbnail[] Thumbnails;
    }
}
