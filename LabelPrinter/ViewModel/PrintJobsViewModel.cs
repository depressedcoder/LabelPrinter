using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LabelPrinter.Drawing;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;

namespace LabelPrinter.ViewModel
{
    public class PrintJobsViewModel : ViewModelBase
    {
        GodexPrinter _printer;
        public RelayCommand PrintJobsButtonCommand { get; }
        public RelayCommand<object> ExitButtonCommand { get; } 

        List<string> _labelSource;
        /// <summary>
        /// List of all LabelNames
        /// </summary>
        public List<string> LabelSource
        {
            get => _labelSource;
            set
            {
                _labelSource = value;
                RaisePropertyChanged(nameof(LabelSource));
            }
        }
         
        private PrintJobsLabel _printJobsLabel;
        /// <summary>
        /// Print jobs property
        /// </summary>
        public PrintJobsLabel PrintJobsLabel
        {
            get => _printJobsLabel;
            set
            {
                _printJobsLabel = value;
                RaisePropertyChanged(nameof(PrintJobsLabel));
            }
        }
        
        readonly StorageSelector _storageSelector;

        public PrintJobsViewModel()
        {
            _printer = new GodexPrinter();

            _storageSelector = new StorageSelector();
            LabelSource = _storageSelector.GetStorage().GetLabelNames();
            InitJobs();
            PrintJobsButtonCommand = new RelayCommand(PrintJobsCommand);
            ExitButtonCommand = new RelayCommand<object>(ExitCommand); 
        }

        private void InitJobs()
        {
            PrintJobsLabel = new PrintJobsLabel
            {
                PrintJobsRow = new List<PrintJobsRow>()
                {
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow(),
                   new PrintJobsRow()
                }
            };
        }
         
        private void PrintJobsCommand()
        {
            var jobs = PrintJobsLabel;
            if(jobs != null && jobs.PrintJobsRow != null)
            {
                var con = StorageSelector.GetConfig();
                var storage = _storageSelector.GetStorage();

                foreach (var job in jobs.PrintJobsRow)
                {
                    if (!string.IsNullOrEmpty(job.LabelName) && job.NoOfCopy > 0)
                    {
                        Label label = storage.GetLabel(job.LabelName);
                        if(label != null)
                        {
                            Print(con, label, job.NoOfCopy);
                        }                        
                    }                    
                }
            }          
        }

        private void Print(Config con, Label label, int noOfCopy)
        {
            SetupPrinter(con, label, noOfCopy);

            _printer.Command.Start();


            var posY = 10;
            var rowHeight = 10;
            var storage = _storageSelector.GetStorage();
            foreach (var labelRow in label.Rows)
            {
                var posX = 10;
                var placeholers = GetPlaceholders(labelRow.Text);

                var strategySelector = new DrawingSelector
                {
                    Graphics = Graphics.FromImage(new Bitmap(label.LabelWidth, label.LabelHeight)),
                    Barcode = label.Barcode,
                    Weight = storage.GetLabels(label.SelectedLabelName).Wieght,
                    Row = labelRow
                };

                foreach (var placeholer in placeholers)
                {
                    var drawingStrategy = strategySelector.GetStrategy(placeholer);

                    drawingStrategy.Print(_printer, ref rowHeight, ref posX, posY);
                }

                posY += rowHeight;
            }

            _printer.Command.End();
            _printer.Close();
        }
        
        private List<string> GetPlaceholders(string input)
        {
            var placeholderPattern = "<([a-zA-Z0-9_.-])+?>";

            var results = new List<string>();

            if (string.IsNullOrEmpty(input))
            {
                return results;
            }

            var matches = Regex.Matches(input, placeholderPattern);

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

                if (!Regex.IsMatch(input, placeholderPattern))
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        results.Add(input);
                    }
                }
            }

            return results;
        }

        private void ExitCommand(object obj)
        {
            Window Win = obj as Window;
            Win.Close();
        }
        
        private void SetupPrinter(Config con, Label label, int numberOfCopy)
        {
            // Connect printer
            PortType port = EnumsConverter.ParseEnum<PortType>(string.IsNullOrEmpty(con.SelectedPrinterPort) ? "USB" : con.SelectedPrinterPort);
            _printer.Open(port);
            //Setup
            PaperMode value = PaperMode.PlainPaperLabel;
            _printer.Config.LabelMode(value, label.LabelHeight, 3); //40-> 
            _printer.Config.LabelWidth(label.LabelWidth); //Label.LabelWidth
            _printer.Config.Dark(con.Density); //con.Density
            _printer.Config.Speed(con.Speed); //con.Speed
            _printer.Config.PageNo(1);
            _printer.Config.CopyNo(numberOfCopy);
            ////Cutting command
            if (label.IsAutomaticCuttingDevice)
            {
                _printer.Config.CutPapaerOn(1);
            }
            else
            {
                _printer.Config.CutPapaerOff();
            }

            _printer.Config.SetLeftMargin(label.DistanceFromLeft); //8 dot = 1 mm
        }

    }
}
