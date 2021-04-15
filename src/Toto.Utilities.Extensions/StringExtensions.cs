namespace Toto.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhitespace(this string item)
        {
            if (string.IsNullOrEmpty(item))
                return true;

            if (string.IsNullOrWhiteSpace(item))
                return true;

            return false;
        }

        public static string SanitizeHtml(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            if (string.IsNullOrWhiteSpace(html))
                return html;
            
            return new HtmlSanitizer().Sanitize(html);
        }
    }
}