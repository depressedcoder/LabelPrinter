using System.Drawing;

namespace LabelPrinter.Drawing
{
    public class TextDrawing : AbstractDrawing
    {
        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                Graphics.DrawString(Placeholder, font, Brushes.Black, new PointF(x, y));

                x += Graphics.MeasureString(Placeholder, font).Width;

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }
    }
}
