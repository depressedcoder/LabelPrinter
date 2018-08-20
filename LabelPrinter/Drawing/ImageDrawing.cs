using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace LabelPrinter.Drawing
{
    public class ImageDrawing : AbstractDrawing
    {
        const string DefaultImage = "Norsel";
        const string ImageNotFound = "<?>";
        const string ImageNamePattern = "<IMG|>";

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            var imageLabel = Regex.Replace(Placeholder, ImageNamePattern, string.Empty);

            if (string.IsNullOrEmpty(imageLabel))
                imageLabel = DefaultImage;

            if (File.Exists($"{imageLabel}.bmp"))
            {
                using (var image = Image.FromFile($"{imageLabel}.bmp"))
                {
                    Graphics.DrawImage(image, x, y, image.Width, image.Height);

                    x += image.Width;

                    if (image.Height > rowHeight)
                    {
                        rowHeight = image.Height;
                    }
                }
            }
            else
            {
                using (var font = GetRowFont())
                {
                    Graphics.DrawString(ImageNotFound, font, Brushes.Black, new PointF(x, y));

                    x += Graphics.MeasureString(ImageNotFound, font).Width;
                }
            }
        }
    }
}
