using GalaSoft.MvvmLight;
using LabelPrinter.Storage;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace LabelPrinter.ViewModel
{
    public partial class SetUpViewModel : ViewModelBase
    {
        public void DataConnectionUpdate()
        {
            if(SelectedDataConnection != "Text Files")
            {
                IsVisibleForConnection = true;
                IsVisibleForLocation = false;
                if (SelectedDataConnection == "Data Base MySQL")
                    EgText = "Example: Server=localhost;Database=labelprinter;Uid=root;Pwd = 4466;";
                if (SelectedDataConnection == "Data Base MS SQL Server")
                    EgText = "Example: Data Source = BS-229; Initial Catalog = LabelPrinter;Integrated Security=True";
                if (SelectedDataConnection == "Data Base Oracle")
                    EgText = "Under Construction....";
            }
            else
            {
                IsVisibleForLocation = true;
                IsVisibleForConnection = false;
            }
        }
        void ChangeCommand()
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
                LocationOfFile = openFolderDialog.SelectedPath + "\\";
        }

        

        void TestConnectionCommand()
        {
            StorageSelector _storageSelector = new StorageSelector();
            var storage = _storageSelector.GetStorage();

            var msg = storage.TestConnection(ODBCConnectionString);

            MessageBox.Show(msg);

        }

        void ExitCommand()
        {

        }

        void SaveCommand()
        {
            var config = new Config
            {
                SelectedConnection = SelectedDataConnection,
                TextConnection = LocationOfFile,
                MssqlConnection = ODBCConnectionString,
                MySqlConnection = ODBCConnectionString,
                OracleConnection = ODBCConnectionString,
                Density = Density,
                Speed = Speed
            };

            File.WriteAllText("Config.json", JsonConvert.SerializeObject(config, Formatting.Indented));

            System.Windows.MessageBox.Show("File saved");
        }
    }
}
