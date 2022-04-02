using Newtonsoft.Json;

namespace tc2
{
    [JsonObject(MemberSerialization.OptIn)]
    class StreamingData
    {
        [JsonProperty("expiresInSeconds")] public int ExpiresIn;
        [JsonProperty("formats")] public Format[] Formats;
        [JsonProperty("adaptiveFormats")] public Format[] AdaptiveFormats;
    }
}
