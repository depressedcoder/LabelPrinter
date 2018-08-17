using LabelPrinter.Helpers;
using LabelPrinter.Model;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class BarcodeStategy : DrawingStrategy
    {
        public override void Draw(Graphics graphics, Barcode barcode, LabelRow row, ref int rowHeight, ref float x, float y)
        {
            using (var barcodeHelper = new BarcodeHelper())
            {
                var barcodeImage = barcodeHelper.GetBarcode(
                    barcode.SelectedBarCode,
                    Placeholder,
                    barcode.CodeSize,
                    barcode.HeightOfCode);

                graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);

                x += barcodeImage.Width;

                if (barcodeImage.Height > rowHeight)
                {
                    rowHeight = barcodeImage.Height;
                }
            }
        }
    }
}
