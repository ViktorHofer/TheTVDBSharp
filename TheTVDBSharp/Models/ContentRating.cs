﻿namespace TheTVDBSharp.Models
{
    /// <summary>
    ///     Different content ratings. View <c>http://en.wikipedia.org/wiki/TV_Parental_Guidelines</c> for more info.
    /// </summary>
    public enum ContentRating
    {
        /// <summary>
        ///     Not suitable for children under 14.
        /// </summary>
        TV14,

        /// <summary>
        ///     This program contains material that parents may find unsuitable for younger children.
        /// </summary>
        TVPG,

        /// <summary>
        ///     This program is designed to be appropriate for all children.
        /// </summary>
        TVY,

        /// <summary>
        ///     This program is designed for children age 7 and above.
        /// </summary>
        TVY7,

        /// <summary>
        ///     Most parents would find this program suitable for all ages.
        /// </summary>
        TVG,

        /// <summary>
        ///     This program is specifically designed to be viewed by adults and therefore may be unsuitable for children under 17.
        /// </summary>
        TVMA
    }

    public static class ContentRatingExtension
    {
        public static ContentRating? ToContentRating(this string rating)
        {
            switch (rating)
            {
                case "TV-14":
                    return ContentRating.TV14;

                case "TV-PG":
                    return ContentRating.TVPG;

                case "TV-Y":
                    return ContentRating.TVY;

                case "TV-Y7":
                    return ContentRating.TVY7;

                case "TV-G":
                    return ContentRating.TVG;

                case "TV-MA":
                    return ContentRating.TVMA;

                default:
                    return null;
            }
        }
    }
}