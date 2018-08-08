using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace LabelPrinter.Helpers
{
    public class BarcodeHelper
    {
        public BitmapImage GetCode39Barcode(string encodeString)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 35, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.CODE39, encodeString, Color.Black, Color.White, 500, 200);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public BitmapImage GetCode128Barcode(string encodeString)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 35, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.CODE128, encodeString, Color.Black, Color.White, 500, 200);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public BitmapImage GetEAN13Barcode(string encodeString)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 35, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.EAN13, encodeString, Color.Black, Color.White, 500, 200);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public BitmapImage GetEAN8Barcode(string encodeString)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 35, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.EAN8, encodeString, Color.Black, Color.White, 500, 200);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public BitmapImage GetInterleaved2of5Barcode(string encodeString)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 35, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.Interleaved2of5, encodeString, Color.Black, Color.White, 500, 200);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

    }
}
