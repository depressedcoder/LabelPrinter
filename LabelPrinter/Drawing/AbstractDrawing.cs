using System.Drawing;
using LabelPrinter.Model;

namespace LabelPrinter.Drawing
{
    public abstract class AbstractDrawing
    {
        public abstract void Draw(
            ref int rowHeight,
            ref float x,
             float y);

        public string Placeholder { get; set; }

        public Graphics Graphics { get; set; }
        public Barcode Barcode { get; set; }
        public LabelRow LabelRow { get; set; }

        protected Font GetRowFont()
        {
            var style = FontStyle.Regular;

            var charWidth = LabelRow.SelectedCharWidth;

            if (LabelRow.IsBold) style |= FontStyle.Bold;

            if (LabelRow.IsUnderlined) style |= FontStyle.Underline;

            if (LabelRow.IsHigh)
            {
                charWidth *= 2;
            }

            var font = new Font("Arial", charWidth, style | style);

            return font;
        }
    }
}
