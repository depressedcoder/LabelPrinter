using LabelPrinter.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace LabelPrinter.Storage
{
    public class MySqlStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            List<String> labelNames = new List<String>();

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "SELECT LABEL_NAME FROM label_in";
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
            return labelNames;
            //throw new NotImplementedException();
        }

        public override Label GetLabel(string labelName)
        {
            var rowLines = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                string query = "SELECT I1,I2,I3,I4,I5,I6,I7,I8,I9,I10,I11,I12,I13,I14,I15 FROM label_out WHERE LABEL_NAME='" + labelName + "'";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
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
           // throw new NotImplementedException();
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
                string query = "INSERT INTO label_in (LABEL_NAME, WEIGHT, LINE1, LINE2, LINE3, LINE4, LINE5, LINE6, LINE7, LINE8, LINE9, LINE10, LINE11, LINE12, LINE13, LINE14, LINE15) VALUES ('" + @label.SelectedLabelName + "','" + @w + "'," + @sb + ")";
                MySqlConnection connection = new MySqlConnection(GetConnectionString());
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            // throw new NotImplementedException();
        }
        
        protected override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.MySqlConnection;

            return connectionString;
            //throw new NotImplementedException();
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
                        return "You have been successfully connected to the MySql database!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //throw new NotImplementedException();
        }
    }
}
