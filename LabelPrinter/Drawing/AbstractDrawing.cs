using System.Drawing;
using LabelPrinter.Model;

namespace LabelPrinter.Drawing
{
    public abstract class AbstractDrawing
    {
        #region Public members

        public string Placeholder { get; set; }
        public decimal Weight { set; get; }
        public Graphics Graphics { get; set; }
        public Barcode Barcode { get; set; }
        public Row Row { get; set; }
        public int Increment { set; get; }

        #endregion

        #region Abstract methods

        public abstract void Draw(ref int rowHeight, ref float x, float y);

        public abstract void Print(GodexPrinter printer, ref int rowHeight, ref int x, int y);

        #endregion

        #region Protected methods

        //Return font for Drawing
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

            var font = new Font("Arial", charWidth, style | style, GraphicsUnit.Point);

            return font;
        }
        //Return font for printing
        protected Font GetRowFontForPrinting()
        {
            var charWidth = Row.SelectedCharWidth * 3;
            if (Row.IsHigh)
            {
                charWidth *= 2;
            }

            var font = new Font("Arial", charWidth, GraphicsUnit.Pixel);

            return font;
        }

        #endregion
    }
}
