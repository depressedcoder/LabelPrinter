using System;
using System.Drawing;
using LabelPrinter.Model;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class DateTimeStrategy : DrawingStrategy
    {
        public override void Draw(Graphics graphics, Barcode barcode, LabelRow row, ref int rowHeight, ref float x, float y)
        {
            var timestamp = string.Empty;

            switch (Placeholder)
            {
                case "<TIMESTAMP>":
                    timestamp = DateTime.Now.ToString("h:mm");
                    break;
                case "<TIME>":
                    timestamp = DateTime.Now.ToString("HH:mm:ss tt");
                    break;
                case "<DATE>":
                    timestamp = DateTime.Now.ToString("dd-mm-yyyy");
                    break;
            }

            var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth);

            graphics.DrawString(timestamp, font, Brushes.Black, new PointF(x, y));

            x += timestamp.Length * font.Size;

            if (font.Height > rowHeight)
            {
                rowHeight = font.Height;
            }
        }
    }
}
