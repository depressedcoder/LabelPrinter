using LabelPrinter.Model;
using LabelPrinter.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace LabelPrinter.DatabaseWatcher
{
    /// <summary>
    /// Observe insertion into LABELS_IN table on MSSQL database
    /// </summary>
    public class MsSqlWatcher : AbstractWatcher
    {
        #region Private members

        private readonly static object _padLoack = new object();
        private static MsSqlWatcher _msSqlDependency;

        #endregion

        #region Constructors

        private MsSqlWatcher()
        {
        }

        public static MsSqlWatcher Instance
        {
            get
            {
                if (_msSqlDependency == null)
                {
                    lock (_padLoack)
                    {
                        if (_msSqlDependency == null)
                        {
                            _msSqlDependency = new MsSqlWatcher();
                        }
                    }
                }
                return _msSqlDependency;
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

            var connectionString = config?.MssqlConnection;

            return connectionString;
        }

        #endregion

        #region Private methods

        private void DetectOnChanged()
        {
            string connectionString = GetConnectionString();
            while (true)
            {
                try
                {
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        var connection = new SqlConnection(connectionString);
                        var command = new SqlCommand();
                        command.Connection = connection;
                        if (connection.State == System.Data.ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        command.CommandText = "select * from dbo.LABELS_IN";
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
                                    if (reader["WEIGHT"] != DBNull.Value)
                                    {
                                        labels.Weight = Convert.ToDecimal(reader["WEIGHT"]);
                                    }
                                    if (reader["Date"] != DBNull.Value)
                                    {
                                        labels.Date = Convert.ToDateTime(reader["Date"]);
                                    }
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
                                        Label label = GetLabel(labels); // JsonConvert.DeserializeObject<Label>(labels.Label);
                                        label.Weight = labels.Weight;
                                        PhysicalPrinter.Instance.Print(label);
                                        var storage = new MsSqlStorage();
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void DeletePreviouse(SqlConnection connection, string labelID)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (SqlCommand command = new SqlCommand("DELETE FROM  dbo.LABELS_IN where ID IN " + labelID, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
