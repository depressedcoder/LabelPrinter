
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace LabelPrinter.Helpers
{
    public class BarcodeHelper : IDisposable
    {
        BarcodeLib.Barcode _barcode;

        public BarcodeHelper()
        {
            _barcode = new BarcodeLib.Barcode
            {
                Alignment = BarcodeLib.AlignmentPositions.LEFT,
                LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER
            };
        }
        /// <summary>
        /// For Generating All the Barcodes
        /// </summary>
        /// <param name="selectedBarcode"></param>
        /// <param name="barCodeLabel"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Image GetBarcode(string selectedBarcode, string barCodeLabel, int width, int height)
        {
            width = width * 100;
            height = height * 20;

            var label = Regex.Replace(Regex.Replace(barCodeLabel, "<BAR|\\++>|>", ""), @"\D", "0");
            var barcodeType = selectedBarcode.ToBarcodeType();
            if (string.IsNullOrEmpty(label))
                label = "0";

            if (barcodeType == BarcodeLib.TYPE.EAN13)
            {
                _barcode.IncludeLabel = true;
                label = label.Substring(0, Math.Min(13, label.Length)).PadLeft(13, '0');
            }

            if (barcodeType == BarcodeLib.TYPE.EAN8)
            {
                _barcode.IncludeLabel = true;
                label = label.Substring(0, Math.Min(8, label.Length)).PadLeft(8, '0');
            }

            if (barcodeType == BarcodeLib.TYPE.Interleaved2of5)
            {
                if (label.Length % 2 != 0)
                {
                    label = "0".Insert(1, label);
                }
            }

            if (barcodeType == BarcodeLib.TYPE.UNSPECIFIED)
            {
                BarcodeDatamatrix barcodeDatamatrix = new BarcodeDatamatrix
                {
                    Height = 24,
                    Width = 24,
                    Options = BarcodeDatamatrix.DM_AUTO,
                    ForceSquareSize = true
                };

                barcodeDatamatrix.Generate(label);
                var image = barcodeDatamatrix.CreateDrawingImage(Color.Black, Color.White);
                return Resize(image, 48, 48, false);
            }

            return _barcode.Encode(barcodeType, label, Color.Black, Color.White, width, height);
        }

        public void Dispose()
        {
            _barcode.Dispose();
        }

        Image Resize(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var result = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(result))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return result;
        }
    }
}
