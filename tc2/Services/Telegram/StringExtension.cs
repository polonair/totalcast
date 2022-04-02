namespace tc2
{
    static class StringExtension
    {
        public static string TelegramMarkDownReplace(this string str)
        {
            return str
                .Replace("+", "\\+").Replace("|", "\\|")
                .Replace("#", "\\#").Replace("=", "\\=")
                .Replace("!", "\\!").Replace("_", "\\_")
                .Replace("(", "\\(").Replace(")", "\\)")
                .Replace(".", "\\.").Replace("-", "\\-");
        }
        public static string TelegramMarkDownTrim(this string str, int length)
        {
            if (str.Length < length) return str;
            string result = str.Substring(0, length - 3);
            while (result[result.Length - 1] == '\\') result = result.Substring(0, result.Length - 1);
            return $"{result}{"...".TelegramMarkDownReplace()}";
        }
    }
}
