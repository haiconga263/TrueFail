namespace Productions.UI.CultivationActivities
{
    public class CultivationActivityUrl
    {
        public const string Prefix = "api/cultivation-activity";

        public const string gets = "gets";
        public static string ApiGets => $"{Prefix}/{gets}";

        public const string get = "get";
        public static string ApiGet => $"{Prefix}/{get}";

        public const string insert = "insert";
        public static string ApiInsert => $"{Prefix}/{insert}";
    }
}
