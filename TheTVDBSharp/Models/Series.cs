using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Models
{
    /// <summary>
    ///     Entity describing a show.
    /// </summary>
    public class Series : IEquatable<Series>
    {
        private readonly uint _id;

        /// <summary>
        ///     Unique identifier used by TVDB and TVDBSharp.
        /// </summary>
        public uint Id
        {
            get { return _id; }
        }

        /// <summary>
        ///     Main language of the show.
        /// </summary>
        public Language? Language { get; set; }

        /// <summary>
        ///     Name of the show.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Link to the banner image.
        /// </summary>
        public string BannerRemotePath { get; set; }

        /// <summary>
        ///     A short overview of the show.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The date the show aired for the first time.
        /// </summary>
        public DateTime? FirstAired { get; set; }

        /// <summary>
        ///     Network that broadcasts the show.
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        ///     Unique identifier used by IMDb.
        /// </summary>
        public string ImdbId { get; set; }

        /// <summary>
        ///     List of all actors in the show.
        /// </summary>
        public IReadOnlyCollection<Actor> Actors { get; set; }

        /// <summary>
        ///     Day of the week when the show airs.
        /// </summary>
        public Frequency? AirDay { get; set; }

        /// <summary>
        ///     Time of the day when the show airs.
        /// </summary>
        public TimeSpan? AirTime { get; set; }

        /// <summary>
        ///     Rating of the content provided by an official organ.
        /// </summary>
        public ContentRating? ContentRating { get; set; }

        /// <summary>
        ///     A list of genres the show is associated with.
        /// </summary>
        public IReadOnlyCollection<string> Genres { get; set; }

        /// <summary>
        ///     Average rating as shown on IMDb.
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        ///     Amount of votes cast.
        /// </summary>
        public int? RatingCount { get; set; }

        /// <summary>
        ///     Let me know if you find out what this is.
        /// </summary>
        public int? Runtime { get; set; }

        /// <summary>
        ///     Current status of the show.
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        ///     Link to a fanart image.
        /// </summary>
        public string FanartRemotePath { get; set; }

        /// <summary>
        ///     Timestamp of the latest update.
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        ///     Let me know if you find out what this is.
        /// </summary>
        public string PosterRemotePath { get; set; }

        /// <summary>
        ///     No clue
        /// </summary>
        public string Zap2ItId { get; set; }

        /// <summary>
        ///     A list of all episodes associated with this show.
        /// </summary>
        public IReadOnlyCollection<Episode> Episodes { get; set; }

        public IReadOnlyCollection<Banner> Banners { get; set; }

        public Series(uint id)
        {
            _id = id;
        }

        public bool Equals(Series other)
        {
            return other != null && _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Series);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}