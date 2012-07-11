using System.IO;

namespace AODL.Document.Helper
{
    public class MimeTypeHelper
    {
        public string GetMediaType(string graphicSourcePath)
        {
            string extenstion = Path.GetExtension(graphicSourcePath).ToLowerInvariant();

            if (extenstion == "jpg" ||
                extenstion == "jpeg")
            {
                return "image/jpeg";
            }
            if (extenstion == "png")
            {
                return "image/png";
            }
            if (extenstion == "gif")
            {
                return "image/gif";
            }
            if (extenstion == "tiff")
            {
                return "image/tiff";
            }
            if (extenstion == "bmp")
            {
                return "image/bmp";
            }

            return "image/jpeg";
        }
    }
}