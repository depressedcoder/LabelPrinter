using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LabelPrinter.Storage
{
    public class StorageSelector
    {
        readonly Dictionary<string, AbstractStorage> _storage;
        readonly Config _config;

        public StorageSelector()
        {
            _storage = new Dictionary<string, AbstractStorage>
            {
                {StorageTypes.Text, new TextStorage() },
                {StorageTypes.MySql, new MySqlStorage() },
                {StorageTypes.Oracle, new OracleStorage() },
                {StorageTypes.MsSql, new MsSqlStorage() }
            };

            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));
        }

        public AbstractStorage GetStorage()
        {
            var selectedStorage = _config?.SelectedDataConnection;

            var strategy = _storage.FirstOrDefault(x => x.Key == selectedStorage).Value ?? new TextStorage();

            return strategy;
        }
    }
}
