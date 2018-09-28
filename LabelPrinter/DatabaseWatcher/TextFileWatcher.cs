using LabelPrinter.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LabelPrinter.DatabaseWatcher
{
    public class TextFileWatcher : AbstractWatcher
    {
        #region private members

        private readonly static object _padLoack = new object();
        private static TextFileWatcher _textFile;

        #endregion

        #region Construcators

        private TextFileWatcher()
        {
        }

        public static TextFileWatcher Instance
        {
            get
            {
                if (_textFile == null)
                {
                    lock (_padLoack)
                    {
                        if (_textFile == null)
                        {
                            _textFile = new TextFileWatcher();
                        }
                    }
                }
                return _textFile;
            }
        }

        #endregion

        #region Public methods

        public override string GetConnectionString()
        {
            string connectionString = string.Empty;
            try
            {
                TextStorage storage = new TextStorage();
                connectionString = storage.GetConnectionString();
                return connectionString;
            }
            catch (Exception ex)
            {
                MessageView.Instance.ShowError(ex.Message);
            }
            return connectionString;
        }

        public override void NotifyNewItem()
        {
            // Just ignore it as we don't need to monitor text file change rather than database
        }

        #endregion
    }
}
