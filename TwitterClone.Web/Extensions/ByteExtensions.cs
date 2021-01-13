namespace TwitterClone.Web.Extensions
{
    using System;
    using System.IO;
    using System.Linq;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;

    public static class ByteExtensions
    {
        public static bool IsValidImage(this byte[] @this)
        {
            var supportedTypes = new string[]
            {
                "image/jpeg",
                "image/png"
            };

            using (var image = Image.Load(@this, out IImageFormat format))
            {
                if (image == null)
                    return false;

                if (supportedTypes.Contains(format.MimeTypes.FirstOrDefault()))
                    return true;
            }

            return false;
        }

        public static string GetMimeTypeFromImageBytes(this byte[] @this)
        {
            if (@this == null || @this.Length == 0)
                return string.Empty;

            try
            {
                using (var image = Image.Load(@this, out IImageFormat format))
                {
                    if (image == null)
                        return string.Empty;

                    var mimeType = format.MimeTypes.FirstOrDefault();
                    return mimeType ?? string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}