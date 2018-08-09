using System.Drawing;

namespace LabelPrinter.Helpers
{
    public static class GraphicsHelper
    {
        public static void Draw(this Graphics graphics, string input, string barcode, bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth, float x, float y)
        {
            var labels = input.Split(' ');

            foreach (var label in labels)
            {
                if (string.IsNullOrEmpty(label))
                {
                    x += 10f;
                    graphics.DrawString(label, GetRowFont(isBold, isUnderLine, isHigh, selectedCharwidth), Brushes.Black, new PointF(x, y));
                }
                else
                {
                    graphics.DrawString(label, GetRowFont(isBold, isUnderLine, isHigh, selectedCharwidth), Brushes.Black, new PointF(x, y));
                }
            }
        }

        static Font GetRowFont(bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth)
        {
            FontStyle style = FontStyle.Regular;
            if (isBold) style |= FontStyle.Bold;
            if (isUnderLine) style |= FontStyle.Underline;
            if (isHigh)
            {
                selectedCharwidth = selectedCharwidth * 2;
            }

            using (var font = new Font("Arial", selectedCharwidth, style | style))
            {
                return font;
            }
        }
    }
}
