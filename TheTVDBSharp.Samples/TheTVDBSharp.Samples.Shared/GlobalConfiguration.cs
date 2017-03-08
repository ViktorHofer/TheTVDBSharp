namespace TheTVDBSharp.Samples
{
    public static partial class GlobalConfiguration
    {
        public static readonly string ApiKey = "";
        public static readonly ITheTVDBManager Manager = new TheTVDBManager(ApiKey);
    }
}
