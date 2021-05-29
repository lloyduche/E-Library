using System;

namespace EBookLibrary.Models.Settings
{
    public class JWTData
    {
        public const string Data = "JWT";
        public static readonly TimeSpan Timespan = new TimeSpan(1, 0, 0);
        public string SymmetricSecurityKey { get; set; }

        public string Issuer { get; set; }
    }
}
