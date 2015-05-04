using System;

namespace TheTVDBSharp.Models
{
    public class Banner : IEquatable<Banner>
    {
        public uint Id { get; }

        public string RemotePath { get; set; }

        public Language? Language { get; set; }

        public double? Rating { get; set; }

        public int? RatingCount { get; set; }

        public Banner(uint id)
        {
            Id = id;
        }

        public bool Equals(Banner other) => other?.Id == Id;

        public override bool Equals(object obj) => Equals(obj as Banner);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
