using System;

namespace TheTVDBSharp.Models
{
    public class Actor : IEquatable<Actor>
    {
        private readonly uint _id;

        public uint Id
        {
            get { return _id; }
        }

        public string ImageRemotePath { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public int SortOrder { get; set; }

        public Actor(uint id)
        {
            _id = id;
        }

        public bool Equals(Actor other)
        {
            return other != null && other._id == _id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Actor);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}
