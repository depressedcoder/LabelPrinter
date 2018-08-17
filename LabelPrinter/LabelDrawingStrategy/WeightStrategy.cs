using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class WeightStrategy : DrawingStrategy
    {
        const string _convertedWeight = "0.0";
        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                Graphics.DrawString(_convertedWeight, font, Brushes.Black, new PointF(x, y));
                
                x +=  Graphics.MeasureString(_convertedWeight, font).Width;

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }
    }
}
