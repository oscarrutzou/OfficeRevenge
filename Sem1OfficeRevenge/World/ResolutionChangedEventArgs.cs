using System;

namespace Sem1OfficeRevenge
{
    public class ResolutionChangedEventArgs : EventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
