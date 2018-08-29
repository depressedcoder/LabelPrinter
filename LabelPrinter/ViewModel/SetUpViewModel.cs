using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace LabelPrinter.ViewModel
{
    public partial class SetUpViewModel : ViewModelBase
    {
        private void ChangeCommand()
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
                LocationOfFile = openFolderDialog.SelectedPath;
        }
        private void TestConnectionCommand()
        {
            System.Windows.MessageBox.Show("Clicked on Test Connection Command.");
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
            System.Windows.MessageBox.Show("File saved in Configure.json");

            
        }
    }
}
