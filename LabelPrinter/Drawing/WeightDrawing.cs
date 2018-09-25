using System.Drawing;

namespace LabelPrinter.Drawing
{
    public class WeightDrawing : AbstractDrawing
    {
        const string ConvertedWeight = "0.0";

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                Graphics.DrawString(Weight.ToString("#,##0.00"), font, Brushes.Black, new PointF(x, y));
                
                x +=  Graphics.MeasureString(Weight.ToString(), font).Width + 2; // 2 * fontSize added because of two zero after weight.

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }

        public override void Print(GodexPrinter printer, ref int rowHeight, ref int x, int y)
        {
            FontWeight BoldStatus = Row.IsBold ? FontWeight.FW_700_BOLD : FontWeight.FW_400_NORMAL;
            Underline_State UnderLineStatus = Row.IsUnderlined ? Underline_State.ON : Underline_State.OFF;
            //if (Row.IsHigh)
            //    Row.SelectedCharWidth *= 2;

            printer.Command.PrintText(x, y, Row.SelectedCharWidth * 3, "Arial", Weight.ToString("#,##0.00"), 0, BoldStatus, RotateMode.Angle_0, Italic_State.OFF, UnderLineStatus, Strikeout_State.OFF, Inverse_State.OFF);

            x += (int)Graphics.MeasureString(Placeholder, GetRowFontForPrinting()).Width;
            if (GetRowFontForPrinting().Height > rowHeight)
            {
                rowHeight = GetRowFontForPrinting().Height;
            }
        }
    }
}
