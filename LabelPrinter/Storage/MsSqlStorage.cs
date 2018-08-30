using LabelPrinter.Model;
using System;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public class MsSqlStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            throw new NotImplementedException();
        }

        public override string[] GetLabelDetails(string labelName)
        {
            throw new NotImplementedException();
        }

        public override void SaveLabel(string file, int howManyCoppies, IEnumerable<LabelRow> allLines)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetLabelDetailsJson(string labelName)
        {
            throw new NotImplementedException();
        }
    }
}
