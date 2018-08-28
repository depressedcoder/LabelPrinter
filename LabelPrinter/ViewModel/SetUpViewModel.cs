using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.Windows;
using System.IO;

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
        void ExitCommand()
        {
          
        }

        private void SaveCommand()
        {
            //saving all info to Configure.json File
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
