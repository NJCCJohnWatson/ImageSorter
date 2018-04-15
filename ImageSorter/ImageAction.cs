using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageSorter
{
    public static class ImageAction
    {
        public static  Boolean IsNotImage(string pathOfimage)
        {
            try
            {
                FileStream fs = new FileStream(pathOfimage, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                buffer = reader.ReadByte();
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();
                if (fileClass == "255216" || fileClass == "7173" || fileClass == "13780" || fileClass == "6677")

                //255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch(Exception e)
            {
                e.ToString();
                return false; 
            }
          
        }
        public static bool IsHorOrNot(Uri uri)
        {
            var decoder = BitmapDecoder.Create(uri, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            var frame = decoder.Frames.FirstOrDefault();
            var height = frame.PixelHeight;
            var width = frame.PixelWidth;
            if (height < width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
