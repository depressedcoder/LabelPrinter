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
        const string TextExtension = "-IMPORT.txt";


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
            
            string query = "SELECT I1,I2,I3,I4,I5,I6,I7,I8,I9,I10,I11,I12,I13,I14,I15 FROM LABEL_OUT WHERE LABEL_NAME='" + labelName + "'";
            Label label;
            SqlConnection connection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rowLines.Add(reader.GetString(0));
                }
                //foreach(var l in label.Rows)
                //{
                   
                //}
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
            try
            {
                string query = "INSERT INTO LABEL_OUT (LABEL_NAME, QTY, I1, I2, I3, I4, I5, I6, I7, I8, I9, I10, I11, I12, I13, I14, I15) VALUES ('"+ @label.SelectedLabelName+"','"+label.HowManyCoppies+"',"+@sb+")";
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

            //if (!string.IsNullOrEmpty(connectionString))
            //    Directory.CreateDirectory(connectionString);
            string con = @"Data Source = BS-229; Initial Catalog = LabelPrinter;Integrated Security=True";

            return connectionString ?? con;

        }
    }
}
