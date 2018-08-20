using LabelPrinter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LabelPrinter.DataConnectionStrategy
{
    public class StrategySelectorForDataConnection
    {
        const string _textFiles = "Text Files";
        const string _dataBaseOracle = "Data Base Oracle";
        const string _dataBaseMySQL = "Data Base MySQL";
        const string _dataBaseMySQLServer = "Data Base MySQL Server";

        ConnectionStrategy con;
        public void getConnectionStrategy(string strategyName)
        {
            switch (strategyName)
            {
                case _textFiles:
                    con = new TextFileStategy();
                    break;
                case _dataBaseOracle:
                    con = new DataBaseOracleStrategy();
                    break;
                case _dataBaseMySQL:
                    con = new DataBaseMySQLStrategy();
                    break;
                case _dataBaseMySQLServer:
                    con = new DataBaseMySQLServerStrategy();
                    break;
                default:
                    break;
            }
            con.unknownMethod();
        }
    }
}
