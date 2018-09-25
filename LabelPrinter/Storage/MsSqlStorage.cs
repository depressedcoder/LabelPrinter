using LabelPrinter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO; 

namespace LabelPrinter.Storage
{
    public class MsSqlStorage : AbstractStorage
    {
        //Label _label;
        //public Label Label
        //{
        //    get => _label;
        //    set { _label = value; }
        //}
      
        public override List<string> GetLabelNames()
        {
            List<String> labelNames = new List<String>();

            try
            {
                string labelName = string.Empty;
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    string query = "SELECT [NAME] FROM LABELS";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            { 
                                labelName = reader["NAME"].ToString();
                                labelNames.Add(labelName);
                            }
                            reader.Close();
                        }
                        connection.Close();
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
            if(labels != null)
                label = JsonConvert.DeserializeObject<Label>(labels.Label); 
            return label;           
        }

        public override Labels GetLabels(string name)
        {
            try
            {
                Labels labels = null;
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    string query = "SELECT * FROM LABELS WHERE [NAME] = '" + name + "'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
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

        public override void SaveLabel(Label label)
        {             
            decimal w = 12;
            try
            {
                Labels dblabels = GetLabels(label.SelectedLabelName); 
                string labelStr = JsonConvert.SerializeObject(label);
                string query = string.Empty;
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    if(dblabels != null)
                    {
                        query = "UPDATE LABELS SET [NAME] = @name, [WEIGHT] = @weight, LABEL = @label WHERE ID = " + dblabels.ID;
                    }
                    else
                    {
                        query = "INSERT INTO LABELS([NAME],[WEIGHT],LABEL) VALUES(@name, @weight, @label)";
                      
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        SqlParameter nameParam = new SqlParameter("@name", label.SelectedLabelName);
                        SqlParameter wightParam = new SqlParameter("@weight", w);
                        SqlParameter labelParam = new SqlParameter("@label", labelStr);

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

            var connectionString = config?.MssqlConnection;

            return connectionString;

        }

        public override string TestConnection(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                    {
                        return "Connection failed.";
                    }
                    else
                    {
                        return "You have been successfully connected to the MsSQL database!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
    }
}
