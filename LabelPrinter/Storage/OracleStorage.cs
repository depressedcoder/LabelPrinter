using Devart.Data.Oracle;
using LabelPrinter.Model;
using Newtonsoft.Json;
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
            //List<String> labelNames = new List<String>();

            //using (OracleConnection connection = new OracleConnection(GetConnectionString()))
            //{
            //    connection.Open();
            //    string query = "SELECT LABEL_NAME FROM label_in";
            //    using (OracleCommand command = new OracleCommand(query, connection))
            //    {
            //        using (OracleDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                labelNames.Add(reader.GetString(0));
            //            }
            //        }
            //    }
            //}
            //return labelNames;
            throw new NotImplementedException();
        }

        public override Label GetLabel(string labelName)
        {
            //var rowLines = new List<string>();

            //using (OracleConnection connection = new OracleConnection(GetConnectionString()))
            //{
            //    string query = "SELECT I1,I2,I3,I4,I5,I6,I7,I8,I9,I10,I11,I12,I13,I14,I15 FROM label_out WHERE LABEL_NAME='" + labelName + "'";
            //    using (OracleCommand command = new OracleCommand(query, connection))
            //    {
            //        connection.Open();

            //        using (OracleDataReader reader = command.ExecuteReader())
            //        {

            //            while (reader.Read())
            //            {
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    rowLines.Add(reader.IsDBNull(i) ? string.Empty : reader[i].ToString());
            //                }
            //            }
            //            var array = rowLines.ToArray();
            //            for (var i = 0; i < array.Length; i++)
            //            {
            //                //foreach(var labelRow in Label.Rows)
            //                //{
            //                //    labelRow.Text = array[i];
            //                //}
            //                //Assign all the textbox value..
            //            }
            //        }
            //    }
            //}
            //return null;
            throw new NotImplementedException();
        }

        public override void SaveLabel(Label label)
        { 
            decimal w = 12;
            try
            {
                string query = string.Empty;
                query = "INSERT INTO LABELS(NAME, WEIGHT, LABEL) VALUES(@name, @weight, @label)";
                OracleConnection connection = new OracleConnection(GetConnectionString());
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("@name", label.SelectedLabelName));
                command.Parameters.Add(new OracleParameter("@weight", w));
                command.Parameters.Add(new OracleParameter("@label", JsonConvert.SerializeObject(label)));
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

            var connectionString = config?.OracleConnection;

            return connectionString;

        }

        public override string TestConnection(string connectionString)
        {
            try
            {
                var str = new DbConnectionStringBuilder(false);
                str.Add("Data Source", "desktop-bi2o5lr");
                str.Add("User ID", "system");
                str.Add("Password", "masud@bs23");
                //var con = new Devart.Data.Oracle.OracleConnection(str.ConnectionString);
                using (OracleConnection connection = new OracleConnection(str.ConnectionString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                    {
                        return "Connection failed.";
                    }
                    else
                    {
                        return "You have been successfully connected to the Oracle database!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            throw new NotImplementedException();
        }

        public override Labels GetLabels(string name)
        {
            throw new NotImplementedException();
        }
    }
}
