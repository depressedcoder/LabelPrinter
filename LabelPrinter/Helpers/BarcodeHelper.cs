using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LabelPrinter.Helpers
{
    public class BarcodeHelper : IDisposable
    {
        BarcodeLib.Barcode _barcode;

        public BarcodeHelper()
        {
            _barcode = new BarcodeLib.Barcode();
            _barcode.Alignment = BarcodeLib.AlignmentPositions.LEFT;
        }

        public Image GetBarcode(string selectedBarcode, string label, int width, int height)
        {
            switch (selectedBarcode)
            {
                case "Code39":
                    if (Regex.IsMatch(label, @"^[-+]?[0-9]*\.?[0-9]+$"))
                        return _barcode.Encode(BarcodeLib.TYPE.CODE39, label, Color.Black, Color.White, width, height);
                    else
                    {
                        return _barcode.Encode(BarcodeLib.TYPE.CODE39, "0", Color.Black, Color.White, width, height);
                    } 
                case "Code128":
                    if (Regex.IsMatch(label, @"^[-+]?[0-9]*\.?[0-9]+$"))
                        return _barcode.Encode(BarcodeLib.TYPE.CODE128, label, Color.Black, Color.White, width, height);
                    else
                        return _barcode.Encode(BarcodeLib.TYPE.CODE128, "0", Color.Black, Color.White, width, height);
                case "EAN13":
                    {
                        if (Regex.IsMatch(label, @"^[-+]?[0-9]*\.?[0-9]+$"))
                        {
                            if (label.Length < 13)
                            {
                                label = label.PadLeft(13, '0');
                            }
                            _barcode.IncludeLabel = true;
                            _barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                            return _barcode.Encode(BarcodeLib.TYPE.EAN13, label, Color.Black, Color.White, width, height);
                        }
                        else
                        {
                            _barcode.IncludeLabel = true;
                            return _barcode.Encode(BarcodeLib.TYPE.EAN13, "0000000000000", Color.Black, Color.White, width, height);
                        }   
                    }
                case "EAN8":
                    {
                        if (Regex.IsMatch(label, @"^[-+]?[0-9]*\.?[0-9]+$"))
                        {
                            if (label.Length < 8)
                            {
                                label = label.PadLeft(8, '0');
                            }
                            _barcode.IncludeLabel = true;
                            return _barcode.Encode(BarcodeLib.TYPE.EAN8, label, Color.Black, Color.White, width, height);
                        }
                        else
                        {
                            _barcode.IncludeLabel = true;
                            return _barcode.Encode(BarcodeLib.TYPE.EAN8, "00000000", Color.Black, Color.White, width, height);
                        } 
                    }
                case "2/5 Interleaved":
                    {
                        if (Regex.IsMatch(label, @"^[-+]?[0-9]*\.?[0-9]+$"))
                        {
                            if (label.Length < 4)
                            {
                                label = label.PadLeft(4, '0');
                            }
                            return _barcode.Encode(BarcodeLib.TYPE.Interleaved2of5, label, Color.Black, Color.White, width, height);
                        }
                        else
                            return _barcode.Encode(BarcodeLib.TYPE.Interleaved2of5, "0000", Color.Black, Color.White, width, height);
                    }
                default:
                    return null;
            }
        }

        public void Dispose()
        {
            _barcode.Dispose();
        }
    }
}
