using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;

namespace LabelPrinter.Drawing
{
    public class BarcodeDrawing : AbstractDrawing
    {
        const string BarcodeMatchingPattern = "<BAR|\\++>|>";

        readonly Dictionary<string, BarcodeLib.TYPE> _barcodeTypes;

        readonly BarcodeLib.Barcode _barcode;

        public BarcodeDrawing()
        {
            _barcodeTypes = new Dictionary<string, BarcodeLib.TYPE>
            {
                {"Code39",  BarcodeLib.TYPE.CODE39},
                {"Code128",  BarcodeLib.TYPE.CODE128},
                {"EAN13",  BarcodeLib.TYPE.EAN13},
                {"EAN8", BarcodeLib.TYPE.EAN8},
                {"2/5 Interleaved",  BarcodeLib.TYPE.Interleaved2of5}
            };

            _barcode = new BarcodeLib.Barcode
            {
                Alignment = BarcodeLib.AlignmentPositions.LEFT,
                LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER
            };
        }

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (_barcode)
            {
                using (var barcodeImage = GetBarcodeImage(Barcode.SelectedBarCode, Placeholder, Barcode.CodeSize, Barcode.HeightOfCode))
                {
                    Graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);

                    x += barcodeImage.Width;

                    if (barcodeImage.Height > rowHeight)
                    {
                        rowHeight = barcodeImage.Height;
                    }
                }
            }
        }

        public override void Print(GodexPrinter printer, ref int rowHeight, ref int x, int y)
        {
            var label = Regex.Replace(Regex.Replace(Placeholder, BarcodeMatchingPattern, ""), @"\D", "0");
            if (Barcode.SelectedBarCode == "Code39")
            {
                printer.Command.PrintBarCode(BarCodeType.Code39, x, y+=40, label);
            }
            else if(Barcode.SelectedBarCode == "Code128")
            {
                printer.Command.PrintBarCode(BarCodeType.Code128_Auto, x, y+=40, label);
            }
            else if(Barcode.SelectedBarCode == "EAN13")
            {
                printer.Command.PrintBarCode(BarCodeType.EAN13, x, y+=40, label);
            }
            else if(Barcode.SelectedBarCode == "EAN8")
            {
                printer.Command.PrintBarCode(BarCodeType.EAN8, x, y+=40, label);
            }
            else if(Barcode.SelectedBarCode == "2/5 Interleaved")
            {
                printer.Command.PrintBarCode(BarCodeType.I2of5, x, y+=40, label);
            }
            else
            {
                System.Windows.MessageBox.Show("Data Matrix is under Construction!!!");
            }
            
        }

        Image GetBarcodeImage(string selectedBarcode, string barCodeLabel, int width, int height)
        {
            width = width * 100;
            height = height * 20;

            var label = Regex.Replace(Regex.Replace(barCodeLabel, BarcodeMatchingPattern, ""), @"\D", "0");

            if (!_barcodeTypes.ContainsKey(selectedBarcode))
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

            var barcodeType = _barcodeTypes[selectedBarcode];

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

            return _barcode.Encode(barcodeType, label, Color.Black, Color.White, width, height);
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
