namespace Productions.UI.Pesticides
{
    public class PesticideUrl
    {
        public const string Prefix = "api/pesticide";

        public const string getAlls = "gets/all";
        public static string ApiGetAlls => $"{Prefix}/{gets}";

        public const string gets = "gets";
        public static string ApiGets => $"{Prefix}/{gets}";

        public const string get = "get";
        public static string ApiGet => $"{Prefix}/{get}";

        public const string insert = "add";
        public static string ApiInsert => $"{Prefix}/{insert}";

        public const string update = "update";
        public static string ApiUpdate => $"{Prefix}/{update}";

        public const string delete = "delete";
        public static string ApiDelete => $"{Prefix}/{delete}";

        public const string getAllCatetories = "category/gets/all";
        public static string ApiGetAllCatetories => $"{Prefix}/{getAllCatetories}";

        public const string getCatetory = "category/get";
        public static string ApiGetCatetory => $"{Prefix}/{getCatetory}";

        public const string getCatetories = "category/gets";
        public static string ApiGetCatetories => $"{Prefix}/{getCatetories}";

        public const string insertCatetory = "category/insert";
        public static string ApiInsertCatetory => $"{Prefix}/{insertCatetory}";

        public const string updateCatetory = "category/update";
        public static string ApiUpdateCatetory => $"{Prefix}/{updateCatetory}";

        public const string deleteCatetory = "category/delete";
        public static string DeleteCatetory => $"{Prefix}/{deleteCatetory}";
    }
}
