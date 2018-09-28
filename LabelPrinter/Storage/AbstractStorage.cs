using LabelPrinter.Model;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public abstract class AbstractStorage
    {
        #region public member(s)

        /// <summary>
        /// Row of a label which will be printed
        /// </summary>
        public Row Row { get; set; }

        #endregion

        #region Abstract method(s)

        public abstract void SaveLabel(Label label);
        public abstract List<string> GetLabelNames();
        public abstract Label GetLabel(string labelName);
        public abstract Labels GetLabels(string name);
        public abstract string GetConnectionString();
        public abstract string TestConnection(string connectionString);
        public abstract bool IsDatabaseConnected(string connectionString);
        public abstract void DeleteLabel(Label label);

        #endregion
    }
}
