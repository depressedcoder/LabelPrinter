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

        public abstract void Print(
            GodexPrinter printer,
            ref int rowHeight,
            ref int x,
            int y);

        public string Placeholder { get; set; }

        public Graphics Graphics { get; set; }
        public Barcode Barcode { get; set; }
        public Row Row { get; set; }

        protected Font GetRowFont()
        {
            var style = FontStyle.Regular;

            var charWidth = Row.SelectedCharWidth;

            if (Row.IsBold) style |= FontStyle.Bold;

            if (Row.IsUnderlined) style |= FontStyle.Underline;

            if (Row.IsHigh)
            {
                charWidth *= 2;
            }
            
            var font = new Font("Arial", charWidth, style | style);

            return font;
        }
    }
}
