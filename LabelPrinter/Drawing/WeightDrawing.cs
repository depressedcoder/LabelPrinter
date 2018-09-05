using System.Drawing;

namespace LabelPrinter.Drawing
{
    public class WeightDrawing : AbstractDrawing
    {
        const string ConvertedWeight = "0.0";

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                Graphics.DrawString(ConvertedWeight, font, Brushes.Black, new PointF(x, y));
                
                x +=  Graphics.MeasureString(ConvertedWeight, font).Width;

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }

        public override void Print(GodexPrinter printer, ref int rowHeight, ref int x, int y)
        {
            //if (Row.SelectedCharWidth > rowHeight)
            //    rowHeight += Row.SelectedCharWidth;

            printer.Command.PrintText(x, y, Row.SelectedCharWidth, "Arial", ConvertedWeight);
            x += (int)Graphics.MeasureString(Placeholder, GetRowFont()).Width;
            if (GetRowFont().Height > rowHeight)
            {
                rowHeight = GetRowFont().Height;
            }
        }
    }
}
