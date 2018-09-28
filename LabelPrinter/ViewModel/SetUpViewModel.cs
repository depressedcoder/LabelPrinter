using GalaSoft.MvvmLight;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace LabelPrinter.ViewModel
{
    public partial class SetUpViewModel : ViewModelBase
    {
        /// <summary>
        /// If class is initialize then config.json should not reset/save
        /// </summary>
        private bool IsNotInit { set; get; }
        private StorageSelector _storageSelector;
        public void DataConnectionUpdate()
        {
            if (SelectedDataConnection != "Text Files")
            {
                Config config = StorageSelector.GetConfig();

                IsVisibleForConnection = true;
                IsVisibleForLocation = false;
                if (config != null)
                {
                    if (SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.MySQL))
                    {
                        EgText = "Example: Server=localhost;Database=labelprinter;Uid=root;Pwd = 4466;";
                        ODBCConnectionString = config.MySqlConnection;
                    }
                    if (SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.MSSQL))
                    {
                        EgText = "Example: Data Source = BS-229; Initial Catalog = LabelPrinter;Integrated Security=True";
                        ODBCConnectionString = config.MssqlConnection;
                    }
                    if (SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.Oracle))
                    {
                        ODBCConnectionString = config.OracleConnection;
                        EgText = "Under Construction....";
                    }
                }
            }
            else
            {
                IsVisibleForLocation = true;
                IsVisibleForConnection = false;
            }

            if (IsNotInit)
            {
                SaveConfig();
            }
            else
            {
                IsNotInit = true;
            }
        }

        private void ChangeCommand()
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                LocationOfFile = openFolderDialog.SelectedPath + "\\";
            }
        }

        private void TestConnectionCommand()
        {
            if (_storageSelector == null)
            {
                _storageSelector = new StorageSelector();
            }

            var storage = _storageSelector.GetStorage();

            var msg = storage.TestConnection(ODBCConnectionString);

            MessageView.Instance.ShowInformation(msg);
        }

        private void ExitCommand(object obj)
        {
            Window Win = obj as Window;

            Win.Close();
        }

        private void SaveCommand(object obj)
        {
            if (_storageSelector == null)
            {
                _storageSelector = new StorageSelector();
            }
            var storage = _storageSelector.GetStorage();
            if (storage.IsDatabaseConnected(ODBCConnectionString))
            {
                SaveConfig();
                MessageView.Instance.ShowInformation("File saved");
                ExitCommand(obj);
            }
            else
            {
                MessageView.Instance.ShowWarning("Connection string is not in correct formate. Pleae enter valid one.");
            }            
        }

        private void SaveConfig()
        {
            Config prevConfig = StorageSelector.GetConfig();
            if (prevConfig != null)
            {
                var config = new Config
                {
                    SelectedConnection = SelectedDataConnection,
                    SelectedScalesPort = SelectedScalesPort,
                    SelectedScalesModel = SelectedScalesModel,
                    SelectedPrinter = SelectedPrinter,
                    SelectedPrinterPort = SelectedPrinterPort,
                    TextConnection = LocationOfFile,
                    MssqlConnection = SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.MSSQL) ? ODBCConnectionString : prevConfig.MssqlConnection,
                    MySqlConnection = SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.MySQL) ? ODBCConnectionString : prevConfig.MySqlConnection,
                    OracleConnection = SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.Oracle) ? ODBCConnectionString : prevConfig.OracleConnection,
                    Density = Density,
                    Speed = Speed,
                    RadioButtonValue = RadioButtonValue,
                    BlackLineText = BlackLineText,
                    GapControlText = GapControlText,
                    LocationOfFile = LocationOfFile,
                    IsCreateOrExport = IsCreateOrExport
                };
                if (!string.IsNullOrEmpty(LocationOfFile))
                {
                    if (!Directory.Exists(LocationOfFile))
                    {
                        Directory.CreateDirectory(LocationOfFile);
                    }

                    File.WriteAllText(LocationOfFile + "\\Config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                else
                {
                    File.WriteAllText("Config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
                }

            }
            else
            {
                throw new FileNotFoundException("Config file not found.");
            }
        }
    }
}
