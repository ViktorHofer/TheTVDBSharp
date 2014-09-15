using System;

namespace TheTVDBSharp.Models
{
    public class Banner : IEquatable<Banner>
    {
        private readonly uint id;

        public uint Id
        {
            get { return this.id; }
        }

        public string RemotePath { get; set; }

        public Language? Language { get; set; }

        public double? Rating { get; set; }

        public int? RatingCount { get; set; }

        public Banner(uint id)
        {
            this.id = id;
        }

        public bool Equals(Banner other)
        {
            return other != null && other.id == this.id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Banner);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }
}
