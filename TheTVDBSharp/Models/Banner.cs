using System;

namespace TheTVDBSharp.Models
{
    public class Banner : IEquatable<Banner>
    {
        private readonly uint _id;

        public uint Id
        {
            get { return _id; }
        }

        public string RemotePath { get; set; }

        public Language? Language { get; set; }

        public double? Rating { get; set; }

        public int? RatingCount { get; set; }

        public Banner(uint id)
        {
            _id = id;
        }

        public bool Equals(Banner other)
        {
            return other != null && other._id == _id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Banner);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}
