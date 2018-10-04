using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabelPrinter.Drawing
{
    public class TextDrawing : AbstractDrawing
    {
        #region public member(s)

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                string label = Placeholder.Replace("++", "");
                Graphics.DrawString(label, font, Brushes.Black, new PointF(x, y));

                x += Graphics.MeasureString(label, font).Width + (label.Count(Char.IsWhiteSpace) * font.Size);

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
            int fontHeight = Row.SelectedCharWidth;
            if (Row.IsHigh)
            {
                fontHeight *= 2;
            }
            bool hasIncrement = Placeholder.Contains("++");
            if (hasIncrement)
            {
                Placeholder = Placeholder.Replace("++", "");
                string resultString = Regex.Match(Placeholder, @"\d+").Value;
                Placeholder = Placeholder.Replace(resultString, (Convert.ToInt16(resultString) + Increment).ToString());
                Increment = 0;
            }
            printer.Command.PrintText(x, y, fontHeight * 3, "Arial", Placeholder, 0, BoldStatus, RotateMode.Angle_0, Italic_State.OFF, UnderLineStatus, Strikeout_State.OFF, Inverse_State.OFF);
            x += (int)Graphics.MeasureString(Placeholder, GetRowFontForPrinting()).Width;
            if (GetRowFontForPrinting().Height > rowHeight)
            {
                rowHeight = GetRowFontForPrinting().Height;
            }
        }

        #endregion
    }
}
