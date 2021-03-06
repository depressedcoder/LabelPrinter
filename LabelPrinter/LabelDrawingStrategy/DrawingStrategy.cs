﻿using LabelPrinter.Model;
using System.Drawing;

namespace LabelPrinter.LabelDrawingStrategy
{
    public abstract class DrawingStrategy
    {
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

            var charWidth = LabelRow.SelectedCharWidth;

            if (LabelRow.IsBold) style |= FontStyle.Bold;

            if (LabelRow.IsUnderlined) style |= FontStyle.Underline;

            if (LabelRow.IsHigh)
            {
                charWidth *= 2;
            }

            var font = new Font("Arial", charWidth, style | style);

            return font;
        }
    }
}
