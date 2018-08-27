using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace LabelPrinter.ViewModel
{
    public partial class SetUpViewModel : ViewModelBase
    {
        private void ChangeCommand()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //    LocationOfFile = openFileDialog.FileName;
        }
        private void TestConnectionCommand()
        {
            MessageBox.Show("Clicked on Test Connection Command.");
        }
        private void ExitCommand()
        {
            MessageBox.Show("You Press on Exit button.");
        }

        private void SaveCommand()
        {
            string strJsonResult = JsonConvert.SerializeObject(
            new
            {
                SelectedScalesModel,
                SelectedScalesPort,
                SelectedPrinter,
                SelectedPrinterPort,
                SelectedDataConnection,
                Density,
                Speed,
                RadioButtonValue,
                BlackLineText,
                GapControlText,
                LocationOfFile,
                ODBCConnectionString,
                IsCreateOrExport
            }
            );
            
            File.WriteAllText("Configure.json", strJsonResult);
            MessageBox.Show("File saved in Configure.json");
        }
    }
}
