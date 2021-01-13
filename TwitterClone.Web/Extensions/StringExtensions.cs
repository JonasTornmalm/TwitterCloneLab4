namespace TwitterClone.Web.Extensions
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        public static bool IsBase64String(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
            {
                return false;
            }

            var buffer = new Span<byte>(new byte[@this.Length]);
            return Convert.TryFromBase64String(@this, buffer, out var _);
        }

        public static string Base64Encode(this string @this)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(@this);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string @this)
        {
            var base64EncodedBytes = Convert.FromBase64String(@this);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
