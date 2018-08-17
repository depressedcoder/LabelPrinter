using LabelPrinter.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class StrategySelector
    {
        const string _barcodeMatchingPattern = "^<BAR.*>";
        const string _imageMatchingPattern = "^<IMG.*>";
        const string _weightMatchingPattern = "^<WEIGHT>";
        const string _datetimeMatchingPattern = "<DATE>|<TIME>|<TIMESTAMP>";

        readonly Dictionary<Predicate<string>, DrawingStrategy> _strategies;

        public Graphics Graphics { get; set; }
        public Barcode Barcode { get; set; }
        public LabelRow LabelRow { get; set; }

        public StrategySelector()
        {
            _strategies = new Dictionary<Predicate<string>, DrawingStrategy>
            {
                { x => Regex.IsMatch(x, _barcodeMatchingPattern), new BarcodeStategy()},
                { x => Regex.IsMatch(x, _imageMatchingPattern), new ImageStrategy() },
                { x => Regex.IsMatch(x, _weightMatchingPattern), new WeightStrategy()},
                { x => Regex.IsMatch(x, _datetimeMatchingPattern), new DateTimeStrategy()}
            };
        }

        public DrawingStrategy GetStrategy(string placeholder)
        {
            var strategy = _strategies.FirstOrDefault(x => x.Key(placeholder)).Value;

            if (strategy == null)
                strategy = new TextStrategy();

            strategy.Placeholder = placeholder;
            strategy.Graphics = Graphics;
            strategy.Barcode = Barcode;
            strategy.LabelRow = LabelRow;

            return strategy;
        }
    }
}
