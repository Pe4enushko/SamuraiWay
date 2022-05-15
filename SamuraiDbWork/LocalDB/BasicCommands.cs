using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SamuraiDbWork.LocalDB
{
    public class BasicCommands
    {
        string GetConnectionString(string id = "SqLiteConnectionString")
        {
            return @"Data Source=.\ayaya.db;Version=3";
            //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public bool IsServerConnected()
        {
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SQLiteException)
                {
                    return false;
                }
            }
        }
        protected DataTable GetData(string command)
        {
            using (IDbConnection Connection = new SQLiteConnection(GetConnectionString()))
            {
                IDbCommand cmd = Connection.CreateCommand();
                cmd.CommandText = command;
                SQLiteDataAdapter DA = new SQLiteDataAdapter((SQLiteCommand)cmd);
                DataTable DT = new DataTable();
                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
                DA.Fill(DT);
                return DT;
            }
        }
        protected bool SendData(string command)
        {
            using (IDbConnection Connection = new SQLiteConnection(GetConnectionString()))
            {
                IDbCommand cmd = Connection.CreateCommand();
                cmd.CommandText = command;
                try
                {
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                    return true;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Message: \n" + exc.Message + "Stack trace: \n" + exc.StackTrace);
                    return false;
                }
            }
        }
        protected bool SendData(string tableName, string[] columns, SQLiteParameter[] parameters)
        {
            using (IDbConnection Connection = new SQLiteConnection(GetConnectionString()))
            {
                IDbCommand cmd = Connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO [{tableName}]({"[" + string.Join("],[", columns) + "]"}) VALUES ( ";
                foreach (SQLiteParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                    cmd.CommandText += param.ParameterName + ",\n";
                }
                cmd.CommandText += new Keys[] { Keys.Back, Keys.Back };
                cmd.CommandText += ");";
                try
                {

                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                    return true;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Message: \n" + exc.Message + "Stack trace: \n" + exc.StackTrace);
                    return false;
                }
            }
        }
    }
}
