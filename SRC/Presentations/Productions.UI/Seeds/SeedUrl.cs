namespace Productions.UI.Seeds
{
    public class SeedUrl
    {
        public const string Prefix = "api/seed";

        public const string getAlls = "gets/all";
        public static string ApiGetAlls => $"{Prefix}/{gets}";

        public const string gets = "gets";
        public static string ApiGets => $"{Prefix}/{gets}";

        public const string get = "get";
        public static string ApiGet => $"{Prefix}/{get}";

        public const string insert = "insert";
        public static string ApiInsert => $"{Prefix}/{insert}";

        public const string update = "update";
        public static string ApiUpdate => $"{Prefix}/{update}";

        public const string delete = "delete";
        public static string ApiDelete => $"{Prefix}/{delete}";
    }
}
