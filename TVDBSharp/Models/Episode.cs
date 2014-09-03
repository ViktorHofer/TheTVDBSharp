using System;
using System.Collections.Generic;

namespace TVDBSharp.Models
{
    /// <summary>
    ///     Entity describing an episode of a <see cref="Series" />show.
    /// </summary>
    public class Episode : IEquatable<Episode>
    {
        private readonly uint id;

        /// <summary>
        ///     Unique identifier for an episode.
        /// </summary>
        public uint Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        ///     This episode's season id.
        /// </summary>
        public uint? SeasonId
        {
            get;
            set;
        }

        /// <summary>
        ///     This episode's season number.
        /// </summary>
        public uint? SeasonNumber
        {
            get;
            set;
        }

        /// <summary>
        ///     This episode's number in the appropriate season.
        /// </summary>
        public int Number
        {
            get;
            set;
        }

        /// <summary>
        ///     Main language spoken in the episode.
        /// </summary>
        public Language? Language
        {
            get;
            set;
        }

        /// <summary>
        ///     This episode's title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        ///     A short description of the episode.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        ///     Path of the episode thumbnail
        /// </summary>
        public string ThumbRemotePath
        {
            get;
            set;
        }

        /// <summary>
        ///     Director of the episode.
        /// </summary>
        public IReadOnlyCollection<string> Directors
        {
            get;
            set;
        }

        /// <summary>
        ///     Writers(s) of the episode.
        /// </summary>
        public IReadOnlyCollection<string> Writers
        {
            get;
            set;
        }

        /// <summary>
        ///     A list of guest stars.
        /// </summary>
        public IReadOnlyCollection<string> GuestStars
        {
            get;
            set;
        }

        /// <summary>
        ///     The date of the first time this episode has aired.
        /// </summary>
        public DateTime? FirstAired
        {
            get;
            set;
        }

        /// <summary>
        ///     Average rating as shown on IMDb.
        /// </summary>
        public double? Rating
        {
            get;
            set;
        }

        /// <summary>
        ///     Amount of votes cast.
        /// </summary>
        public int? RatingCount
        {
            get;
            set;
        }

        /// <summary>
        ///     Timestamp of the last update to this episode.
        /// </summary>
        public DateTime? LastUpdated
        {
            get;
            set;
        }

        /// <summary>
        ///     Width dimension of the thumbnail in pixels;
        /// </summary>
        public int? ThumbWidth
        {
            get;
            set;
        }

        /// <summary>
        ///     Height dimension of the thumbnail in pixels.
        /// </summary>
        public int? ThumbHeight
        {
            get;
            set;
        }

        public Episode(uint id)
        {
            this.id = id;
        }

        public bool Equals(Episode other)
        {
            return other != null && this.id == other.id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Episode);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }
}