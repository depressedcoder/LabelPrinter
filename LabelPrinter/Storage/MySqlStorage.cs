using LabelPrinter.Model;
using System;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public class MySqlStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            throw new NotImplementedException();
        }

        public override List<LabelRow> GetLabelDetails(string labelName)
        {
            throw new NotImplementedException();
        }

        public override void SaveLabel(string labelName, int numberOfCopies, IEnumerable<LabelRow> labelRows)
        {
            throw new NotImplementedException();
        }
        
        protected override string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
