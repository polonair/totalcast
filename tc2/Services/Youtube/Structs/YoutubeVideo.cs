using Newtonsoft.Json;

namespace tc2
{
    [JsonObject(MemberSerialization.OptIn)]
    class YoutubeVideo
    {
        [JsonProperty("streamingData")] public StreamingData StreamingData;
        [JsonProperty("videoDetails")] public VideoDetails Details;
    }
}
