using LabelPrinter.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace LabelPrinter.Storage
{
    public class MySqlStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            List<String> labelNames = new List<String>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT NAME FROM LABELS";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                labelNames.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return labelNames;
        }

        public override Label GetLabel(string labelName)
        {
            Label label = null;
            Labels labels = GetLabels(labelName);
            if (labels != null)
                label = JsonConvert.DeserializeObject<Label>(labels.Label);

            return label;
        }

        public override Labels GetLabels(string name)
        {
            Labels labels = null;
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                string query = "SELECT * FROM LABELS WHERE NAME = '" + name + "'";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            labels = new Labels();
                            labels.ID = Convert.ToInt64(reader["ID"]);
                            labels.Name = reader.IsDBNull(1) ? string.Empty : reader["NAME"].ToString();
                            labels.Wieght = reader.IsDBNull(2) ? 0 : Convert.ToDecimal(reader["WEIGHT"]);
                            labels.Label = reader.IsDBNull(3) ? string.Empty : reader["LABEL"].ToString();
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return labels;
        }

        public override void SaveLabel(Label label)
        {
            decimal w = 12;
            try
            {

                Labels dblabels = GetLabels(label.SelectedLabelName);
                string labelStr = JsonConvert.SerializeObject(label);
                string query = string.Empty;
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    if (dblabels != null)
                    {
                        query = "UPDATE LABELS SET NAME = @name, WEIGHT = @weight, LABEL = @label WHERE ID = " + dblabels.ID;
                    }
                    else
                    {
                        query = "INSERT INTO LABELS(NAME,WEIGHT,LABEL) VALUES(@name, @weight, @label)";

                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        MySqlParameter nameParam = new MySqlParameter("@name", label.SelectedLabelName);
                        MySqlParameter wightParam = new MySqlParameter("@weight", w);
                        MySqlParameter labelParam = new MySqlParameter("@label", labelStr);

                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(wightParam);
                        command.Parameters.Add(labelParam);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        protected override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.MySqlConnection;

            return connectionString;
        }

        public override string TestConnection(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                    {
                        return "Connection failed.";
                    }
                    else
                    {
                        connection.Close();
                        return "You have been successfully connected to the MySql database!";
                    }
                }
            }
            catch (MySqlException exc)
            {
                return "Connection failed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
