using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using LabelPrinter.Model;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class ImageStrategy : DrawingStrategy
    {
        public override void Draw(Graphics graphics, Barcode barcode, LabelRow row, ref int rowHeight, ref float x, float y)
        {
            var imageLabel = Regex.Replace(Placeholder, "<IMG|>", "");

            if (File.Exists($"{imageLabel}.bmp"))
            {
                var image = Image.FromFile($"{imageLabel}.bmp");
                graphics.DrawImage(image, x, y, image.Width, image.Height);

                x += image.Width;

                if (image.Height > rowHeight)
                {
                    rowHeight = image.Height;
                }
            }
            else if (Placeholder == "<IMG>")
            {
                var image = Image.FromFile("Norsel.bmp");
                graphics.DrawImage(image, x, y, image.Width, image.Height);

                x += image.Width;

                if (image.Height > rowHeight)
                {
                    rowHeight = image.Height;
                }
            }
            else
            {
                using (var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth))
                {
                    graphics.DrawString("<?>", font, Brushes.Black, new PointF(x, y));

                    x += Placeholder.Length * font.Size;
                }
            }
        }
    }
}
