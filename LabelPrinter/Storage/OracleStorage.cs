 
using LabelPrinter.Model;
using Newtonsoft.Json;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;

namespace LabelPrinter.Storage
{
    public class OracleStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            List<String> labelNames = new List<String>();
            string labelName = string.Empty;
            try
            {
                using (OracleConnection connection = new OracleConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT NAME FROM labelprinter.LABELS_OUT";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                labelName = reader["NAME"].ToString();
                                labelNames.Add(labelName);
                            }
                        }
                    }
                    connection.Close();
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

        public override void SaveLabel(Label label)
        { 
            decimal w = 12;
            try
            {
                Labels dblabels = GetLabels(label.SelectedLabelName);
                string labelStr = JsonConvert.SerializeObject(label);
                string query = string.Empty;
                using(OracleConnection connection = new OracleConnection(GetConnectionString()))
                {
                    if (dblabels != null)
                    {
                        query = "UPDATE labelprinter.LABELS_OUT SET NAME = :1, WEIGHT = :2, LABEL = :3 WHERE ID = " + dblabels.ID;
                    }
                    else
                    {
                        query = "INSERT INTO labelprinter.LABELS_OUT(NAME, WEIGHT, LABEL) VALUES(:1, :2, :3)";

                    }
                    using( OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2 , label.SelectedLabelName, ParameterDirection.Input));
                        command.Parameters.Add(new OracleParameter("2", OracleDbType.Decimal , w, ParameterDirection.Input));
                        command.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, JsonConvert.SerializeObject(label), ParameterDirection.Input));

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }        
                }                
            }
            catch (Exception ex)
            {
                MessageView.Instance.ShowError(ex.Message);
            }
        }

        public override void DeleteLabel(Label label)
        {
            try
            {
                Labels dblabels = GetLabels(label.SelectedLabelName.Trim()); 
                string query = string.Empty;
                if(dblabels != null)
                {
                    using (OracleConnection connection = new OracleConnection(GetConnectionString()))
                    {
                        query = "delete from labelprinter.LABELS_OUT WHERE ID = :1";

                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            OracleParameter IDParam = new OracleParameter("1", dblabels.ID);

                            command.Parameters.Add(IDParam);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageView.Instance.ShowInformation("Delete Successful.");
                        }
                    }
                }
                else
                {
                    MessageView.Instance.ShowWarning("The label already deleted from database.");
                }                
            }
            catch (Exception ex)
            {
                MessageView.Instance.ShowError(ex.Message);
            }
        }

        public override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.OracleConnection;

            return connectionString;

        }

        public override string TestConnection(string connectionString)
        {
            try
            { 
                if (!IsDatabaseConnected(connectionString))
                {
                    return "Connection failed.";
                }
                else
                {
                    return "You have been successfully connected to the Oracle database!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 
        }

        public override bool IsDatabaseConnected(string connectionString)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    bool isConnected = connection.State == ConnectionState.Open;
                    if (isConnected)
                        connection.Close();
                    return isConnected;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override Labels GetLabels(string name)
        {
            try
            {
                Labels labels = null;
                using (OracleConnection connection = new OracleConnection(GetConnectionString()))
                {
                    string query = "SELECT * FROM labelprinter.LABELS_OUT WHERE NAME = '" + name + "'";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        connection.Open();

                        using (OracleDataReader reader = command.ExecuteReader())
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
