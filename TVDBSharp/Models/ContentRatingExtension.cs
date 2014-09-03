
namespace TVDBSharp.Models
{
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
