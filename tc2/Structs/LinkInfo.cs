namespace tc2
{
    class LinkInfo
    {
        public LinkType Type { get; internal set; }
        public string Title { get; internal set; }
        public bool IsSubscribed { get; internal set; }
        public string Service { get; internal set; }
        public string SourceId { get; internal set; }
    }
}
