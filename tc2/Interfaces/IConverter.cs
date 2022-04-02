namespace tc2
{
    interface IConverter
    {
        Content Convert(Item item, Content loaded);
        bool CanConvertFrom(MimeType mime);
    }
}
