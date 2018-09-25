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

        public StorageSelector()
        {
            _storage = new Dictionary<string, AbstractStorage>
            {
                {StorageTypes.Text, new TextStorage() },
                {StorageTypes.MySql, new MySqlStorage() },
                {StorageTypes.Oracle, new OracleStorage() },
                {StorageTypes.MsSql, new MsSqlStorage() }
            };
        }

        public AbstractStorage GetStorage()
        {
            Config config = GetConfig();
            var selectedStorage = config?.SelectedConnection;

            var strategy = _storage.FirstOrDefault(x => x.Key == selectedStorage).Value ?? new TextStorage();

            return strategy;
        }

        public static Config GetConfig()
        {

            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));
            return config;
        }
    }
}
