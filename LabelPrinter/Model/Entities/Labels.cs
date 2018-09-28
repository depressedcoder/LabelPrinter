
using System;

namespace LabelPrinter.Model
{
    /// <summary>
    /// This entity is used to menipulate from database with labels_in and labels_out table
    /// </summary>
    public class Labels
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public decimal Wieght { get; set; }
        public string Label { get; set; }  
    }
}
