using Newtonsoft.Json;

namespace tc2
{
    [JsonObject(MemberSerialization.OptIn)]
    class Thumbnail
    {
        [JsonProperty("url")] public string Url;
        [JsonProperty("width")] public int Width;
        [JsonProperty("height")] public int Height;
    }
}
