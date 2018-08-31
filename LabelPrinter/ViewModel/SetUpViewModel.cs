using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace LabelPrinter.ViewModel
{
    public partial class SetUpViewModel : ViewModelBase
    {
        void ChangeCommand()
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
                LocationOfFile = openFolderDialog.SelectedPath + "\\";
        }

        void TestConnectionCommand()
        {
            System.Windows.MessageBox.Show("Clicked on Test Connection Command.");
        }

        void ExitCommand()
        {

        }

        void SaveCommand()
        {
            var config = new Config
            {
                SelectedConnection = SelectedDataConnection,
                TextConnection = LocationOfFile
            };

            File.WriteAllText("Config.json", JsonConvert.SerializeObject(config, Formatting.Indented));

            System.Windows.MessageBox.Show("File saved");
        }
    }
}
