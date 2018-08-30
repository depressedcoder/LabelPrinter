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

        public override void GetLabelDetails(string labelName)
        {
            throw new NotImplementedException();
        }

        public override void SaveLabel()
        {
            throw new NotImplementedException();
        }
    }
}
