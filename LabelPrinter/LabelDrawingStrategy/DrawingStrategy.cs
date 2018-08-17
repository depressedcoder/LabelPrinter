using LabelPrinter.Model;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public abstract class DrawingStrategy
    {
        /// <summary>
        /// Used For Drawing the Barcodes,Images and texts
        /// </summary>
        /// <param name="x">Drawing position of the Label</param>
        /// <param name="y">Drawing position of the Label</param>
        /// <returns>Returns the Next Rows starting point of Y coordinate</returns>
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
            FontStyle style = FontStyle.Regular;

            if (LabelRow.IsBold) style |= FontStyle.Bold;

            if (LabelRow.IsUnderlined) style |= FontStyle.Underline;

            if (LabelRow.IsHigh)
            {
                LabelRow.SelectedCharWidth *= 2;
            }

            var font = new Font("Arial", LabelRow.SelectedCharWidth, style | style);

            return font;
        }
    }
}
