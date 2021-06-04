namespace CQuerMVC.Helpers
{
    public static class BooleanExtensions
    {
        private static readonly string POSITIVE_RESPONSE = "Yes";
        private static readonly string NEGATIVE_RESPONSE = "No";
        public static string ToYesNoString(this bool value)
        {
            return value ? POSITIVE_RESPONSE : NEGATIVE_RESPONSE;
        }
    }
}
