using System.Collections.Generic;

namespace LabelPrinter.Model
{
    public class LabelRow
    {
        public string Text { get; set; }
        public List<int> CharWidths { get; set; } = new List<int> { 6, 8, 10, 12, 15, 18, 20, 24 };
        public int SelectedCharWidth { get; set; }
        public bool IsHigh { get; set; }
        public bool IsBold { get; set; }
        public bool IsUnderlined { get; set; }
    }
}
