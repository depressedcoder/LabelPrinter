

using LabelPrinter.DatabaseWatcher;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace LabelPrinter.DatabaseWatcher
{
    public class MsSqlWatcher : AbstractWatcher
    {
        private readonly static object _padLoack = new object();
        private static MsSqlWatcher _msSqlDependency; 

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

        public override void NotifyNewItem()
        {
            Thread thread = new Thread(() => DetectOnChanged());
            thread.Start();
        }

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

                        command.CommandText = "select ID, NAME, WEIGHT, LABEL from dbo.LABELS_IN";
                        command.CommandType = System.Data.CommandType.Text;

                        List<string> IDList = new List<string>();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Labels labels = new Labels();
                                while (reader.Read())
                                {
                                    labels.ID = Convert.ToInt32(reader["ID"]);
                                    labels.Name = reader["Name"].ToString();
                                    labels.Wieght = Convert.ToDecimal(reader["WEIGHT"]);
                                    labels.Label = reader["LABEL"].ToString();
                                    if (!string.IsNullOrEmpty(labels.Label))
                                    {
                                        Label label = JsonConvert.DeserializeObject<Label>(labels.Label);
                                        PhysicalPrinter.Print(label);
                                        var storage = new MsSqlStorage();
                                        storage.SaveLabel(label);
                                        IDList.Add(labels.ID.ToString());
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
    }
}
