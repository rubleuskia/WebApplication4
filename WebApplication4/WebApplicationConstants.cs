namespace WebApplication4
{
    public static class WebApplicationConstants
    {
        public static class Roles
        {
            public const string Administrator = "admin";
        }

        public static class Files
        {
            public const string ProtectedStaticFilesPath = "ProtectedStaticFiles";

            public const string PublicStaticImagesPath = "images";
            public static readonly string ProtectedStaticImagesPath = $"/{ProtectedStaticFilesPath}/images/";
        }
    }
}