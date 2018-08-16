using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabelPrinter.LabelDrawingStrategy
{
    public class StrategySelector
    {
        Dictionary<Predicate<string>, IDrawingStrategy> _stategies;
        public StrategySelector()
        {
            _stategies = new Dictionary<Predicate<string>, IDrawingStrategy>
            {
                { x => Regex.IsMatch(x, "^<BAR.*>"), new BarcodeStategy()},
                {x => Regex.IsMatch(x, "^<IMG.*>"), new ImageStrategy() },
                { x => Regex.IsMatch(x, "^<WEIGHT>"), new WeightStrategy()},
                { x => Regex.IsMatch(x, "<DATE>|<TIME>|<TIMESTAMP>"), new DateTimeStrategy()}
            };
        }

        public IDrawingStrategy GetStrategy(string input)
        {
            return _stategies.First(x => x.Key(input)).Value;
        }

        List<string> GetPlaceholders(string input)
        {
            var results = new List<string>();

            if (string.IsNullOrEmpty(input))
            {
                return results;
            }

            var matches = Regex.Matches(input, "<.+?>");

            if (matches.Count == 0)
            {
                results.Add(input);
                return results;
            }

            for (var i = 0; i < matches.Count; i++)
            {
                var idx = input.IndexOf(matches[i].Value, System.StringComparison.Ordinal);

                var token = input.Substring(0, idx);

                if (!string.IsNullOrEmpty(token))
                {
                    results.Add(token);
                }

                results.Add(matches[i].Value);

                input = input.Substring(idx + matches[i].Value.Length);

                if (!Regex.IsMatch(input, "<.+?>"))
                {
                    if (!string.IsNullOrEmpty(input))
                        results.Add(input);
                }
            }
            return results;
        }
    }
}
