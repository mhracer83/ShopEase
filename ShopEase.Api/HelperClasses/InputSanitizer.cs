namespace ShopEase.Api.HelperClasses
{
    public static class InputSanitizer
    {
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            // Remove any HTML tags
            input = System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);

            // Remove any script tags
            input = System.Text.RegularExpressions.Regex.Replace(
                input,
                "<script.*?</script>",
                string.Empty,
                System.Text.RegularExpressions.RegexOptions.Singleline
                    | System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );

            // Trim whitespace
            input = input.Trim();

            return input;
        }
    }
}
