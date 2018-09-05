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

        public override void Print(GodexPrinter printer, ref int rowHeight, ref int x, int y)
        {
            if (Row.SelectedCharWidth > rowHeight)
                rowHeight += Row.SelectedCharWidth;

            printer.Command.PrintText(x, y, Row.SelectedCharWidth, "Arial", Placeholder);
            
        }
    }
}
