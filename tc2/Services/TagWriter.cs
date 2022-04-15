using Id3;
using Id3.Frames;

namespace tc2
{
    class TagWriter : IService, ITagWriter
    {
        public void WriteTags(Item item, Content converted)
        {
            using (var mp3 = new Mp3(converted.Stream, Mp3Permissions.ReadWrite))
            {
                Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
                if (tag == null) tag = new Id3Tag();
                tag.Clear();
                tag.Title = new TitleFrame() { Value = item.Title, EncodingType = Id3TextEncoding.Unicode };
                tag.Artists = new ArtistsFrame() { EncodingType = Id3TextEncoding.Unicode };
                tag.Artists.Value.Add(item.Author);
                mp3.WriteTag(tag, Id3Version.V1X, WriteConflictAction.Replace);
                mp3.WriteTag(tag, Id3Version.V23, WriteConflictAction.Replace);
            }
        }
    }
}
