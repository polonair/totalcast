using System;

namespace tc2
{
    class CheckMimeEventArgs : EventArgs
    {
        public MimeType InputMime { get; internal set; }
        public bool CanProcess { get; internal set; } = false;
    }
}
