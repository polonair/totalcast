using Newtonsoft.Json;

namespace tc2
{
    [JsonObject(MemberSerialization.OptIn)]
    class Format
    {
        [JsonProperty("itag")] public int ITag;
        [JsonProperty("url")] public string Url;
        [JsonProperty("height")] public int Height;
        [JsonProperty("width")] public int Width;
        [JsonProperty("approxDurationMs")] public int ApproxDurationMs;
        [JsonProperty("audioChannels")] public int AudioChannels;
        [JsonProperty("audioQuality")] public string AudioQuality;
        [JsonProperty("audioSampleRate")] public int AudioSampleRate;
        [JsonProperty("averageBitrate")] public int AverageBitrate;
        [JsonProperty("bitrate")] public int Bitrate;
        [JsonProperty("contentLength")] public long ContentLength;
        [JsonProperty("fps")] public int Fps;
        [JsonProperty("highReplication")] public bool HighReplication;
        [JsonProperty("lastModified")] public long LastModified;
        [JsonProperty("loudnessDb")] public double LoudnessDb;
        [JsonProperty("mimeType")] public string MimeType;
        [JsonProperty("quality")] public string Quality;
        [JsonProperty("qualityLabel")] public string QualityLabel;
        [JsonProperty("signatureCipher")] public string SignatureCipher;
    }
}
