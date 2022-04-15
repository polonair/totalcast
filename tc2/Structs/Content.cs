using System.IO;

namespace tc2
{
    class Content
    {
        public MimeType Mime { get; internal set; }
        public Stream Stream { get; internal set; }
        public int SampleRate { get; internal set; }
        public int Channels { get; internal set; }
        public int Duration { get; internal set; }
    }
}
