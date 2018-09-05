using LabelPrinter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

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

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "SELECT LABEL_NAME FROM LABEL_OUT";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            labelNames.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return labelNames;
        }

        public override Label GetLabel(string labelName)
        {
            var rowLines = new List<string>();
            
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string query = "SELECT I1,I2,I3,I4,I5,I6,I7,I8,I9,I10,I11,I12,I13,I14,I15 FROM LABEL_OUT WHERE LABEL_NAME='" + labelName + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowLines.Add(reader.IsDBNull(i) ? string.Empty : reader[i].ToString());
                            }
                        }
                        var array = rowLines.ToArray();
                        for (var i = 0; i < array.Length; i++)
                        {
                            //foreach(var labelRow in Label.Rows)
                            //{
                            //    labelRow.Text = array[i];
                            //}
                            //Assign all the textbox value..
                        }
                    }
                }
            }
                return null;
           
        }

        public override void SaveLabel(Label label)
        {
            string sep = "'";
            StringBuilder sb = new StringBuilder();
            foreach (var l in label.Rows)
            {
                sb.Append(sep).Append(l.Text);
                sep = "','";
            }
            sb.Append("'");

            decimal w = 12;
            try
            {
                string query = "INSERT INTO LABEL_IN (LABEL_NAME, WEIGHT, LINE1, LINE2, LINE3, LINE4, LINE5, LINE6, LINE7, LINE8, LINE9, LINE10, LINE11, LINE12, LINE13, LINE14, LINE15) VALUES ('" + @label.SelectedLabelName+ "','"+@w+"',"+@sb+")";
                SqlConnection connection = new SqlConnection(GetConnectionString());
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
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
                        return "You have been successfully connected to the database!";
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
