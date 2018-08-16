using LabelPrinter.Model;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public interface IDrawingStrategy
    {
        /// <summary>
        /// Used For Drawing the Barcodes,Images and texts
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="barcode">Design of the barcode</param>
        /// <param name="x">Drawing position of the Label</param>
        /// <param name="y">Drawing position of the Label</param>
        /// <returns>Returns the Next Rows starting point of Y coordinate</returns>
        PointF Draw(
            Graphics graphics,
            Barcode barcode,
            LabelRow row,
            float x,
            float y);
    }
}
