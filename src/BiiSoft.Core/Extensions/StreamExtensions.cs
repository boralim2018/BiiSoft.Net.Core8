using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Extensions
{
    public static class StreamExtensions
    {
        //public static Stream ToFixed(this Stream imageStream, int Width, SKEncodedImageFormat format)
        //{
        //    using (var inputStream = new SKManagedStream(imageStream))
        //    {
        //        using (var original = SKBitmap.Decode(inputStream))
        //        {
        //            int width, height;
        //            if (original.Width > original.Height) //landscape
        //            {
        //                width = Width;
        //                height = original.Height * Width / original.Width;
        //            }
        //            else
        //            {
        //                width = original.Width * Width / original.Height;
        //                height = Width;
        //            }

        //            using (var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High))
        //            {
        //                if (resized == null) return null;

        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    resized.Encode(format, 100).SaveTo(memoryStream);
        //                    return memoryStream;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
