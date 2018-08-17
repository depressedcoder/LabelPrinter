using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class StrategySelector
    {
        Dictionary<Predicate<string>, DrawingStrategy> _stategies;

        public StrategySelector()
        {
            _stategies = new Dictionary<Predicate<string>, DrawingStrategy>
            {
                { x => Regex.IsMatch(x, "^<BAR.*>"), new BarcodeStategy()},
                { x => Regex.IsMatch(x, "^<IMG.*>"), new ImageStrategy() },
                { x => Regex.IsMatch(x, "^<WEIGHT>"), new WeightStrategy()},
                { x => Regex.IsMatch(x, "<DATE>|<TIME>|<TIMESTAMP>"), new DateTimeStrategy()}
            };
        }

        public DrawingStrategy GetStrategy(string placeholder)
        {
            var strategy = _stategies.FirstOrDefault(x => x.Key(placeholder)).Value;

            if (strategy == null)
                strategy = new TextStrategy();

            strategy.Placeholder = placeholder;

            return strategy;
        }
    }
}
