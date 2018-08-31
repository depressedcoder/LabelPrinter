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

        public override LabelDetails GetLabelDetails(string labelName)
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
