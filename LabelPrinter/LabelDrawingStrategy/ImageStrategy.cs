using System;
using System.Drawing;
using LabelPrinter.Model;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class ImageStrategy : IDrawingStrategy
    {
        public PointF Draw(Graphics graphics, Barcode barcode, LabelRow row, float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
