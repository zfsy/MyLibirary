using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace MyLibirary
{
    public class ImgUtil
    {
        public static Image ImgStringToImage(string imageB64)
        {
            byte[] imgBytes = Convert.FromBase64String(imageB64);
            Image result = null;
            using (MemoryStream ms = new MemoryStream(imgBytes))
            {
                result = Image.FromStream(ms);
            }

            return result;
        }
    }
}
