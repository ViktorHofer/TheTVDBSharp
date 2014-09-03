using System;

namespace TheTVDBSharp.Models
{
    public abstract class BannerBase : IEquatable<BannerBase>
    {
        private readonly uint id;

        public uint Id
        {
            get
            {
                return this.id;
            }
        }

        public string RemotePath
        {
            get;
            set;
        }

        public Language? Language
        {
            get;
            set;
        }

        public double? Rating
        {
            get;
            set;
        }

        public int? RatingCount
        {
            get;
            set;
        }

        public BannerBase(uint id)
        {
            this.id = id;
        }

        public bool Equals(BannerBase other)
        {
            return other != null && other.id == this.id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as BannerBase);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }
}
