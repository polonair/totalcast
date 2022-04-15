using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;
using System.IO;

namespace tc2
{
    class FFMpegConverter : IService, IConverter
    {
        class ARG_AC1_FMP3 : IArgument { public string Text => "-ac 1 -f mp3"; }
        public bool CanConvertFrom(MimeType mime)
            => (mime == MimeType.AudioWebmOpus) 
            || (mime == MimeType.AudioMp4Mp4a402);
        public Content Convert(Item item, Content content)
        {
            int br = 8 * 45 * 1000 / content.Duration;
            if (br > 64) br = 64;
            int sr = br < 24 ? 22050 : 44100;
            MemoryStream encoded = new();
            FFMpegArguments
                .FromPipeInput(new StreamPipeSource(content.Stream))
                .OutputToPipe(new StreamPipeSink(encoded), options => options
                    .WithAudioCodec(AudioCodec.LibMp3Lame)
                    .WithAudioSamplingRate(sr)
                    .WithAudioBitrate(br)
                    .WithArgument(new ARG_AC1_FMP3()))
                .ProcessSynchronously();
            return new Content() { Channels = 1, Mime = MimeType.AudioMp3, SampleRate = sr, Stream = encoded, Duration = content.Duration };
        }
    }
}
