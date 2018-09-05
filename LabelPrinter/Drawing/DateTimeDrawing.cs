﻿using System;
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
            
            //if (Row.SelectedCharWidth > rowHeight)
            //    rowHeight += Row.SelectedCharWidth;

            var datetime = DateTime.Now.ToString(_datetimeFormat[Placeholder]);
            printer.Command.PrintText(x, y, Row.SelectedCharWidth, "Arial", datetime);
            
            x +=(int) Graphics.MeasureString(datetime, GetRowFont()).Width;

            if (GetRowFont().Height>rowHeight)
            {
                rowHeight = GetRowFont().Height;
            }


        }
    }
}
