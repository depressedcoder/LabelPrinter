

using LabelPrinter.Drawing;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabelPrinter
{
    public class PhysicalPrinter
    {
        #region private member(s)

        private static GodexPrinter _printer;
        private static StorageSelector _storageSelector;
        private readonly static object _padLoack = new object();
        private static PhysicalPrinter _physicalPrinter;

        #endregion

        private PhysicalPrinter()
        {
        }

        public static PhysicalPrinter Instance
        {
            get
            {
                if (_physicalPrinter == null)
                {
                    lock (_padLoack)
                    {
                        if (_physicalPrinter == null)
                        {
                            _physicalPrinter = new PhysicalPrinter();
                        }
                    }
                }
                return _physicalPrinter;
            }
        }


        #region public member(s)

        public void Print(Label label, int noOfCopy)
        {
            var con = StorageSelector.GetConfig();
            _printer = new GodexPrinter();
            _storageSelector = new StorageSelector();

            SetupPrinter(con, label, noOfCopy);

            //Print
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
                    Weight = storage.GetLabels(label.SelectedLabelName)?.Wieght ?? 0,
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

        public void Print(Label label)
        {
            var con = StorageSelector.GetConfig();
            _printer = new GodexPrinter();
            _storageSelector = new StorageSelector();

            SetupPrinter(con, label, label.HowManyCoppies);

            //Print
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
                    Weight = storage.GetLabels(label.SelectedLabelName)?.Wieght ?? 0,
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

        public void PrintJob(Label label, int noOfCopy)
        {
            Row row = label.Rows.Where(r => r.Text.Contains("++")).FirstOrDefault();
            if (row != null)
            {
                var con = StorageSelector.GetConfig();
                for (int i = 0; i < noOfCopy; i++)
                {
                    _printer = new GodexPrinter();
                    _storageSelector = new StorageSelector();

                    SetupPrinter(con, label, noOfCopy);

                    //Print
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
                            Weight = storage.GetLabels(label.SelectedLabelName)?.Wieght ?? 0,
                            Row = labelRow
                        };

                        foreach (var placeholer in placeholers)
                        {
                            var drawingStrategy = strategySelector.GetStrategy(placeholer);

                            drawingStrategy.Increment = i;
                            drawingStrategy.Print(_printer, ref rowHeight, ref posX, posY);
                        }

                        posY += rowHeight;
                    }

                    _printer.Command.End();
                    _printer.Close();
                }
            }
            else
            {
                Print(label, noOfCopy);
            }

        }

        public List<string> GetPlaceholders(string input)
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

        #endregion

        #region private method(s)

        private static void SetupPrinter(Config con, Label label, int numberOfCopy)
        {
            // Connect printer
            PortType port = EnumsConverter.ParseEnum<PortType>(string.IsNullOrEmpty(con.SelectedPrinterPort) ? "USB" : con.SelectedPrinterPort);
            _printer.Open(port);
            //Setup
            PaperMode value = EnumsConverter.ParseEnum<PaperMode>(con.RadioButtonValue.ToString());
            int gapFeed = Convert.ToInt32(string.IsNullOrEmpty(con.GapControlText) ? "0" : con.GapControlText);
            _printer.Config.LabelMode(value, label.LabelHeight, gapFeed); //40-> 
            _printer.Config.LabelWidth(label.LabelWidth); //Label.LabelWidth
            _printer.Config.Dark(con.Density); //con.Density
            _printer.Config.Speed(con.Speed); //con.Speed
            _printer.Config.PageNo(1);
            _printer.Config.CopyNo(numberOfCopy);
            if (label.SelectedPrinterType == EnumsConverter.GetDescription(PrinterType.DirectThermal))
            {
                _printer.Config.SetPrinterType("D"); // direct tharmul
            }
            else
            {
                _printer.Config.SetPrinterType("T");
            }

            ////Cutting command
            if (label.IsAutomaticCuttingDevice)
            {
                _printer.Config.CutPapaerOn(1);
            }
            else
            {
                _printer.Config.CutPapaerOff();
            }

            _printer.Config.SetLeftMargin(label.DistanceFromLeft * 8); //8 dot = 1 mm
        }

        #endregion
    }
}
