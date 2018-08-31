namespace LabelPrinter
{
    public class Config
    {
        public string SelectedScalesModel { get; set; }
        public string SelectedScalesPort { get; set; }
        public string SelectedPrinter { get; set; }
        public string SelectedPrinterPort { get; set; }
        public string SelectedConnection { get; set; }
        public int Density { get; set; }
        public int Speed { get; set; }
        public int RadioButtonValue { get; set; }
        public string BlackLineText { get; set; }
        public string GapControlText { get; set; }
        public string LocationOfFile { get; set; }
        public string OracleConnection { get; set; }
        public string MySqlConnection { get; set; }
        public string MssqlConnection { get; set; }
        public string TextConnection { get; set; }
        public bool IsCreateOrExport { get; set; }
    }
}
