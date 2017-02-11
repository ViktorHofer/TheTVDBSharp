using System;

namespace TheTVDBSharp.Models
{
    public class Actor : IEquatable<Actor>
    {
        public uint Id { get; }

        public string ImageRemotePath { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public int SortOrder { get; set; }

        public Actor(uint id)
        {
            Id = id;
        }

        public bool Equals(Actor other) => other?.Id == Id;

        public override bool Equals(object obj) => Equals(obj as Actor);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
