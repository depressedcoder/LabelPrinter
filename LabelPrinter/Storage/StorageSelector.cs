using System.Collections.Generic;
using System.Linq;

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

        public AbstractStorage GetStorage(string storageKey)
        {
            return _storage.FirstOrDefault(x => x.Key == storageKey).Value ?? new TextStorage();
        }
    }
}
