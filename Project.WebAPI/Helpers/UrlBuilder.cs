namespace Project.WebAPI.Helpers
{
    public static class UrlBuilder
    {
        private const string ApplicationUrl = "http://localhost:4200";


        public static string GenerateEmailConfirmationLink(long userId, string code)
        {
            return $"{ApplicationUrl}/ConfirmEmail?userId={userId}&code={code}";
        }

        public static string GenerateResetPasswordLink(long userId, string code)
        {
            return $"{ApplicationUrl}/ResetPassword?userId={userId}&code={code}";
        }
    }
}