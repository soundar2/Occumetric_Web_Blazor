using System.Linq;
using System;
using System.Collections.Generic;
using IdentityServer4.Extensions;

namespace Occumetric.Server.Areas.Shared
{
    public class PdaUtility
    {
        public static int SanitizeString(string str)
        {
            if (str.IsNullOrEmpty()) return 0;
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