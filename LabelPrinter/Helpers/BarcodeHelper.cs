using System;
using System.Drawing;

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
                        _barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
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
