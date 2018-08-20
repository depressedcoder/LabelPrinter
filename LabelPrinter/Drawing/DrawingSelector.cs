using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using LabelPrinter.Model;

namespace LabelPrinter.Drawing
{
    public class DrawingSelector
    {
        const string BarcodeMatchingPattern = "^<BAR.*>";
        const string ImageMatchingPattern = "^<IMG.*>";
        const string WeightMatchingPattern = "^<WEIGHT>";
        const string DatetimeMatchingPattern = "<DATE>|<TIME>|<TIMESTAMP>";

        readonly Dictionary<Predicate<string>, AbstractDrawing> _strategies;

        public Graphics Graphics { get; set; }
        public Barcode Barcode { get; set; }
        public LabelRow LabelRow { get; set; }

        public DrawingSelector()
        {
            _strategies = new Dictionary<Predicate<string>, AbstractDrawing>
            {
                { x => Regex.IsMatch(x, BarcodeMatchingPattern), new BarcodeDrawing()},
                { x => Regex.IsMatch(x, ImageMatchingPattern), new ImageDrawing() },
                { x => Regex.IsMatch(x, WeightMatchingPattern), new WeightDrawing()},
                { x => Regex.IsMatch(x, DatetimeMatchingPattern), new DateTimeDrawing()}
            };
        }

        public AbstractDrawing GetStrategy(string placeholder)
        {
            var strategy = _strategies.FirstOrDefault(x => x.Key(placeholder)).Value ?? new TextDrawing();

            strategy.Placeholder = placeholder;
            strategy.Graphics = Graphics;
            strategy.Barcode = Barcode;
            strategy.LabelRow = LabelRow;

            return strategy;
        }
    }
}
