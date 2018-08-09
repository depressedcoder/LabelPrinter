using System.Drawing;

namespace LabelPrinter.Helpers
{
    public class BarcodeHelper
    {
        public Image GetBarcode(string selectedBarcode, string label, int codeSize, int heightOfCode)
        {
            if (selectedBarcode == "Code39")
                return GetCode39Barcode(label, codeSize, heightOfCode);

            return null;
        }

        public Image GetCode39Barcode(string encodeString, int codeSize, int heightOfCode)
        {
            heightOfCode = heightOfCode * 50;
            codeSize = codeSize * 100;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 20, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.CODE39, encodeString, Color.Black, Color.White, codeSize, heightOfCode);

            return image;
        }
        public Image GetCode128Barcode(string encodeString, int codeSize, int heightOfCode)
        {
            heightOfCode = heightOfCode * 50;
            codeSize = codeSize * 100;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 20, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.CODE128, encodeString, Color.Black, Color.White, codeSize, heightOfCode);

            return image;
        }
        public Image GetEAN13Barcode(string encodeString, int codeSize, int heightOfCode)
        {
            heightOfCode = heightOfCode * 50;
            codeSize = codeSize * 100;
            if (encodeString.Length < 13)
            {
                encodeString = encodeString.PadLeft(13, '0');
            }
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 20, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.EAN13, encodeString, Color.Black, Color.White, codeSize, heightOfCode);

            return image;
        }
        public Image GetEAN8Barcode(string encodeString, int codeSize, int heightOfCode)
        {
            heightOfCode = heightOfCode * 50;
            codeSize = codeSize * 100;
            if (encodeString.Length < 8)
            {
                encodeString = encodeString.PadLeft(8, '0');
            }
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 20, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.EAN8, encodeString, Color.Black, Color.White, codeSize, heightOfCode);

            return image;
        }
        public Image GetInterleaved2of5Barcode(string encodeString, int codeSize, int heightOfCode)
        {
            heightOfCode = heightOfCode * 50;
            codeSize = codeSize * 100;
            if (encodeString.Length < 4)
            {
                encodeString = encodeString.PadLeft(4, '0');
            }
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.LabelFont = new Font("Sample Bar Code Font", 20, FontStyle.Bold);
            b.IncludeLabel = true;
            Image image = b.Encode(BarcodeLib.TYPE.Interleaved2of5, encodeString, Color.Black, Color.White, codeSize, heightOfCode);

            return image;
        }

    }
}
