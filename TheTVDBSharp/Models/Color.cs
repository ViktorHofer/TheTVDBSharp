
namespace TheTVDBSharp.Models
{
    public class Color
    {
        public byte R
        {
            get;
            set;
        }

        public byte G
        {
            get;
            set;
        }

        public byte B
        {
            get;
            set;
        }

        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
    }
}
