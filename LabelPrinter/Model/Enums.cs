using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LabelPrinter.Model
{
    public enum DataConnections
    {
        [Description("Text Files")]
        TextFile = 0,
        [Description("Data Base Oracle")]
        Oracle = 1,
        [Description("Data Base MySQL")]
        MySQL = 2,
        [Description("Data Base MS SQL Server")]
        MSSQL = 3
    }

    public enum Watcher
    {
        MsSqlWatcher,
        MySqlWatcher,
        OracleWatcher
    }

    public enum PrinterType
    {
        [Description("Direct Thermal")]
        DirectThermal = 1,

        [Description("Thermal Transfer")]
        ThermalTransfer = 2
    }
}
