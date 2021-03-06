﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class DateTimeStrategy : DrawingStrategy
    {
        readonly Dictionary<string, string> _datetimeFormat;

        public DateTimeStrategy()
        {
            _datetimeFormat = new Dictionary<string, string>
            {
                {"<TIMESTAMP>","h:mm" },
                {"<TIME>", "HH:mm:ss tt" },
                {"<DATE>",  "dd-mm-yyyy"}
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
    }
}
