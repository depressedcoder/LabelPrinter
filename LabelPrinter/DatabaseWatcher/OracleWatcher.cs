using LabelPrinter.Model;
using LabelPrinter.Storage;
using Newtonsoft.Json;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace LabelPrinter.DatabaseWatcher
{
    /// <summary>
    /// Observe insertion into LABELS_IN table on oracle database
    /// </summary>
    public class OracleWatcher : AbstractWatcher
    {
        #region Private members

        private static readonly object _padLoack = new object();
        private static OracleWatcher _oracleWather;

        #endregion

        #region Construcators

        private OracleWatcher()
        {
        }

        public static OracleWatcher Instance
        {
            get
            {
                if (_oracleWather == null)
                {
                    lock (_padLoack)
                    {
                        if (_oracleWather == null)
                        {
                            _oracleWather = new OracleWatcher();
                        }
                    }
                }
                return _oracleWather;
            }
        }

        #endregion

        #region Public methods

        public override void NotifyNewItem()
        {
            Thread thread = new Thread(() => DetectOnChanged());
            thread.Start();
        }

        public override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
            {
                throw new ArgumentException("Configuration file is missing");
            }

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.OracleConnection;

            return connectionString;
        }

        #endregion

        #region Private methods

        private void DetectOnChanged()
        {
            string connectionString = GetConnectionString();
            while (true)
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    var connection = new OracleConnection(connectionString);
                    var command = new OracleCommand();
                    command.Connection = connection;
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    command.CommandText = "select ID, NAME, WEIGHT, LABEL from labelprinter.LABELS_IN";
                    command.CommandType = System.Data.CommandType.Text;

                    List<string> IDList = new List<string>();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            LabelIn labels = new LabelIn();
                            while (reader.Read())
                            {
                                labels.Id = Convert.ToInt32(reader["ID"]);
                                labels.Name = reader["Name"].ToString();
                                labels.Weight = Convert.ToDecimal(reader["WEIGHT"]);
                                labels.Date = Convert.ToDateTime(reader["Date"]);
                                labels.Line1 = reader["Line1"].ToString();
                                labels.Line2 = reader["Line2"].ToString();
                                labels.Line3 = reader["Line3"].ToString();
                                labels.Line4 = reader["Line4"].ToString();
                                labels.Line5 = reader["Line5"].ToString();
                                labels.Line6 = reader["Line6"].ToString();
                                labels.Line7 = reader["Line7"].ToString();
                                labels.Line8 = reader["Line8"].ToString();
                                labels.Line9 = reader["Line9"].ToString();
                                labels.Line10 = reader["Line10"].ToString();
                                labels.Line11 = reader["Line11"].ToString();
                                labels.Line12 = reader["Line12"].ToString();
                                labels.Line13 = reader["Line13"].ToString();
                                labels.Line14 = reader["Line14"].ToString();
                                labels.Line15 = reader["Line15"].ToString();

                                if (!string.IsNullOrEmpty(labels.Name))
                                {
                                    Label label = GetLabel(labels); //JsonConvert.DeserializeObject<Label>(labels.Label);
                                    label.Weight = labels.Weight;
                                    PhysicalPrinter.Instance.Print(label);
                                    var storage = new OracleStorage();
                                    storage.SaveLabel(label);
                                    IDList.Add(labels.Id.ToString());
                                }
                            }
                        }
                    }

                    if (IDList != null && IDList.Count > 0)
                    {
                        string ids = "( " + string.Join(",", IDList.ToArray()) + " )";
                        DeletePreviouse(connection, ids);
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }


        private void DeletePreviouse(OracleConnection connection, string labelID)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (OracleCommand command = new OracleCommand("DELETE FROM  labelprinter.LABELS_IN where ID IN " + labelID, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        #endregion  
    }
}
