using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class TextStrategy : DrawingStrategy
    {
        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                Graphics.DrawString(Placeholder, font, Brushes.Black, new PointF(x, y));

                x += Placeholder.Length * font.Size;

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }
    }
}
