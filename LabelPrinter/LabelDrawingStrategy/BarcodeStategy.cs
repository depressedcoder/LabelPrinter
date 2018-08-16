using LabelPrinter.Helpers;
using LabelPrinter.Model;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class BarcodeStategy : IDrawingStrategy
    {
        //public PointF Draw(
        //    Graphics graphics, 
        //    string input, 
        //    string barcode, 
        //    bool isBold, 
        //    bool isUnderLine, 
        //    bool isHigh, 
        //    int selectedCharwidth, 
        //    float x, 
        //    float y)
        //{
        //    using (var barcodeHelper = new BarcodeHelper())
        //    {
        //        var barcodeImage = barcodeHelper.GetBarcode(barcode, input, 2, 5);
        //        graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);

        //        x += barcodeImage.Width;

        //        //if (barcodeImage.Height > rowHeight)
        //        //{
        //        //    rowHeight = barcodeImage.Height;
        //        //}
        //    }
        //    return new PointF(x, y);
        //}

        public PointF Draw(Graphics graphics, Barcode barcode, LabelRow row, float x, float y)
        {
            throw new System.NotImplementedException();
        }
    }
}
