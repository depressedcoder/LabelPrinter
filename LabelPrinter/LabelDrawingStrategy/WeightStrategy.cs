using System.Drawing;
using LabelPrinter.Model;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class WeightStrategy : DrawingStrategy
    {
        public override void Draw(Graphics graphics, Barcode barcode, LabelRow row, ref int rowHeight, ref float x, float y)
        {
            using(var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth))
            {
                graphics.DrawString("0.0", font, Brushes.Black, new PointF(x, y));

                x += Placeholder.Length * font.Size;

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }
    }
}
