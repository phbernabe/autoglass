using System.Text.RegularExpressions;

namespace DesafioAutoglass.Application.Helpers
{
    public static class DataHelper
    {
        public static string OnlyDigits(string data)
        {
            return string.IsNullOrEmpty(data) ? null : Regex.Replace(data, "[^\\d]", "");
        }
    }
}
