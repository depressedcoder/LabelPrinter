using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class ImageStrategy : DrawingStrategy
    {
        const string defaultImage = "Norsel";
        const string imageNotFound = "<?>";
        const string imageNamePattern = "<IMG|>";

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            var imageLabel = Regex.Replace(Placeholder, imageNamePattern, string.Empty);

            if (string.IsNullOrEmpty(imageLabel))
                imageLabel = defaultImage;

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
                    Graphics.DrawString(imageNotFound, font, Brushes.Black, new PointF(x, y));

                    x += Placeholder.Length * font.Size;
                }
            }
        }
    }
}
