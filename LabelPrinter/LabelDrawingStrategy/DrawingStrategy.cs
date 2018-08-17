using LabelPrinter.Model;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public abstract class DrawingStrategy
    {
        /// <summary>
        /// Used For Drawing the Barcodes,Images and texts
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="barcode">Design of the barcode</param>
        /// <param name="x">Drawing position of the Label</param>
        /// <param name="y">Drawing position of the Label</param>
        /// <returns>Returns the Next Rows starting point of Y coordinate</returns>
        public abstract void Draw(
             Graphics graphics,
             Barcode barcode,
             LabelRow row,
            ref int rowHeight,
            ref float x,
             float y);

        public string Placeholder { get; set; }

        protected Font GetRowFont(bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth)
        {
            FontStyle style = FontStyle.Regular;

            if (isBold) style |= FontStyle.Bold;

            if (isUnderLine) style |= FontStyle.Underline;

            if (isHigh)
            {
                selectedCharwidth = selectedCharwidth * 2;
            }

            var font = new Font("Arial", selectedCharwidth, style | style);

            return font;
        }
    }
}
