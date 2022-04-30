using System;
using System.Data;
using System.Data.SQLite;

namespace SamuraiDbWork
{
    public class BasicCommands
    {
        SQLiteConnection Connection;
        public BasicCommands(string dbpath)
        {
            Connection = new SQLiteConnection($@"Data Source = {dbpath};Version=3;New=True;Compress=True;");
        }
        protected DataTable GetData(string command)
        {
            SQLiteCommand cmd = Connection.CreateCommand();
            cmd.CommandText = command;
            SQLiteDataAdapter DA = new SQLiteDataAdapter(cmd);
            DataTable DT = new DataTable();
            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
            DA.Fill(DT);
            return DT;
        }
        protected void SendData(string command)
        {
            SQLiteCommand cmd = Connection.CreateCommand();
            cmd.CommandText = command;
            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
