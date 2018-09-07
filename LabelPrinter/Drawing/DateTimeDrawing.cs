using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LabelPrinter.Drawing
{
    public class DateTimeDrawing : AbstractDrawing
    {
        readonly Dictionary<string, string> _datetimeFormat;

        public DateTimeDrawing()
        {
            _datetimeFormat = new Dictionary<string, string>
            {
                {"<TIMESTAMP>","h:mm" },
                {"<TIME>", "HH:mm:ss tt" },
                {"<DATE>",  "dd-MM-yyyy"}
            };
        }

        public override void Draw(ref int rowHeight, ref float x, float y)
        {
            using (var font = GetRowFont())
            {
                var datetime = DateTime.Now.ToString(_datetimeFormat[Placeholder]);

                Graphics.DrawString(datetime, font, Brushes.Black, new PointF(x, y));

                x += Graphics.MeasureString(datetime, font).Width;

                if (font.Height > rowHeight)
                {
                    rowHeight = font.Height;
                }
            }
        }

        public override void Print(GodexPrinter printer, ref int rowHeight, ref int x, int y)
        {
            var datetime = DateTime.Now.ToString(_datetimeFormat[Placeholder]);
            FontWeight BoldStatus = Row.IsBold ? FontWeight.FW_700_BOLD : FontWeight.FW_400_NORMAL;
            Underline_State UnderLineStatus = Row.IsUnderlined ? Underline_State.ON : Underline_State.OFF;
            //if (Row.IsHigh)
            //    Row.SelectedCharWidth *= 2;

            printer.Command.PrintText(x, y, Row.SelectedCharWidth * 3, "Arial", datetime, 0, BoldStatus, RotateMode.Angle_0, Italic_State.OFF, UnderLineStatus, Strikeout_State.OFF, Inverse_State.OFF);
            
            x +=(int) Graphics.MeasureString(datetime, GetRowFontForPrinting()).Width;

            if (GetRowFontForPrinting().Height>rowHeight)
            {
                rowHeight = GetRowFontForPrinting().Height;
            }


        }
    }
}
