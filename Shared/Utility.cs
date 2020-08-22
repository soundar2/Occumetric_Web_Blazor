namespace Occumetric.Shared
{
    public class Utility
    {
        public static int SanitizeStringToInteger(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            str = str.ToLower()
                .Replace("\"", string.Empty)
                .Replace(",", string.Empty)
                .Replace("inches", string.Empty)
                .Replace("waist", "36")
                .Replace("floor", "0")
                .Replace("shoulder", "60")
                .Replace("overhead", "80")
                .Replace("head", "65");

            return int.TryParse(str, out int result) ? result : 0;
        }
    }
}
