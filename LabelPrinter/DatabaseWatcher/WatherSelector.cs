using LabelPrinter.Storage;
using System.Collections.Generic;
using System.Linq;

namespace LabelPrinter.DatabaseWatcher
{
    public class WatherSelector
    {
        private static WatherSelector _watherSelector;
        private readonly static object _padLoack = new object();
        readonly Dictionary<string, AbstractWatcher> _watcher;
        private WatherSelector()
        {
            _watcher = new Dictionary<string, AbstractWatcher>();
            _watcher.Add(StorageTypes.Text, TextFileWatcher.Instance);
            _watcher.Add(StorageTypes.MySql, MySqlWatcher.Instance);
            _watcher.Add(StorageTypes.MsSql, MsSqlWatcher.Instance);
            _watcher.Add(StorageTypes.Oracle, OracleWatcher.Instance);
        }

        public static WatherSelector Instance
        {
            get
            {
                if(_watherSelector == null)
                {
                    lock (_padLoack)
                    {
                        if(_watherSelector == null)
                        {
                            _watherSelector = new WatherSelector();
                        }
                    }
                }
                return _watherSelector;
            }
        }

        public AbstractWatcher GetWatcher()
        {
            Config config = StorageSelector.GetConfig();
            var selectedWather = config?.SelectedConnection;

            var wather = _watcher.FirstOrDefault(x => x.Key == selectedWather).Value ?? TextFileWatcher.Instance;

            return wather;
        }
    }
}
