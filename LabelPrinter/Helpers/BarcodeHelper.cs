
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

        public Image GetBarcode(string selectedBarcode, string barCodeLabel, int width, int height)
        {
            width = width * 100;
            height = height * 20;

            var label = Regex.Replace(Regex.Replace(barCodeLabel, "<BAR|\\++>|>", ""), @"\D", "0");

            if (string.IsNullOrEmpty(label))
                label = "0";

            switch (selectedBarcode)
            {
                case "Code39":
                    return _barcode.Encode(BarcodeLib.TYPE.CODE39, label, Color.Black, Color.White, width, height);
                case "Code128":
                    return _barcode.Encode(BarcodeLib.TYPE.CODE128, label, Color.Black, Color.White, width, height);
                case "EAN13":
                    {
                        if (label.Length < 13)
                        {
                            label = label.PadLeft(13, '0');
                        }
                        _barcode.IncludeLabel = true;
                        return _barcode.Encode(BarcodeLib.TYPE.EAN13, label, Color.Black, Color.White, width, height);
                    }
                case "EAN8":
                    {
                        if (label.Length < 8)
                        {
                            label = label.PadLeft(8, '0');
                        }
                        _barcode.IncludeLabel = true;
                        return _barcode.Encode(BarcodeLib.TYPE.EAN8, label, Color.Black, Color.White, width, height);
                    }
                case "2/5 Interleaved":
                    {
                        if (label.Length < 4)
                        {
                            label = label.PadLeft(4, '0');
                        }
                        return _barcode.Encode(BarcodeLib.TYPE.Interleaved2of5, label, Color.Black, Color.White, width, height);
                    }
                case "DataMatrix":
                    {
                        BarcodeDatamatrix barcodeDatamatrix = new BarcodeDatamatrix
                        {
                            Height = 24,
                            Width = 24,
                            Options = BarcodeDatamatrix.DM_AUTO,
                            ForceSquareSize = true
                        };

                        //https://online-barcode-reader.inliteresearch.com/
                        barcodeDatamatrix.Generate(label);
                        var image = barcodeDatamatrix.CreateDrawingImage(Color.Black, Color.White);
                        var newImage = Resize(image, 48, 48, false);
                        return newImage;
                    }
                default:
                    return null;
            }
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
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }
    }
}
