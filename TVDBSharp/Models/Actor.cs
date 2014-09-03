using System;

namespace TVDBSharp.Models
{
    public class Actor : IEquatable<Actor>
    {
        private readonly uint id;

        public uint Id
        {
            get
            {
                return this.id;
            }
        }

        public string ImageRemotePath
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }

        public int SortOrder
        {
            get;
            set;
        }

        public Actor(uint id)
        {
            this.id = id;
        }

        public bool Equals(Actor other)
        {
            return other != null && other.id == this.id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Actor);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }
}
