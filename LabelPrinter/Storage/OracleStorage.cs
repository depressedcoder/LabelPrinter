using LabelPrinter.Model;
using System;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public class OracleStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            throw new NotImplementedException();
        }

        public override Label GetLabel(string labelName)
        {
            throw new NotImplementedException();
        }

        public override void SaveLabel(Label label)
        {
            throw new NotImplementedException();
        }
        
        protected override string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public override string TestConnection(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
