namespace tc2
{
    class MimeType
    {
        public static readonly MimeType AudioWebmOpus = "audio/webm; codecs=\"opus\"";
        public static readonly MimeType AudioMp4Mp4a402 = "audio/mp4; codecs=\"mp4a.40.2\"";
        public static readonly MimeType AudioMp3 = "audio/mpeg";

        public string Data { get; private set; }

        private MimeType() { }
        public override bool Equals(object obj) => (obj is MimeType mime) && (this.Data == mime.Data);
        public static bool operator ==(MimeType a, MimeType b) => a.Equals(b);
        public static bool operator !=(MimeType a, MimeType b) => !a.Equals(b);
        public static implicit operator string(MimeType a) => a.Data;
        public static implicit operator MimeType(string a) => new() { Data = a };
    }
}
