using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SamuraiDbWork.GlobalDB
{
    public class BasicCommands
    {
        string GetConnectionString(string id = "MSSQLConnectionString")
        {
            return "workstation id=SamuraiPathDB.mssql.somee.com;" +
"packet size = 4096;"+
"user id = Pe4enushko_SQLLogin_1;"+
"pwd = 5ruvigkb4y;"+
"data source = SamuraiPathDB.mssql.somee.com;"+
"persist security info = False;"+
"initial catalog = SamuraiPathDB";
            //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public bool IsServerConnected()
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        protected DataTable GetData(string command)
        {
            using(SqlConnection Connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = Connection.CreateCommand();
                cmd.CommandText = command;
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
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
            using (SqlConnection Connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = Connection.CreateCommand();
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
        protected bool SendData(string tableName, string[] columns ,SqlParameter[] parameters)
        {
            using (SqlConnection Connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = Connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO [{tableName}]({"[" + string.Join("],[",columns) + "]"}) VALUES ( ";
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                    cmd.CommandText += param.ParameterName + ",\n";
                }
                cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.Length - 2, 2);
                cmd.CommandText += ");";
                try
                {

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Message: \n" + exc.Message + "Stack trace: \n" + exc.StackTrace);
                    return false;
                }
            }
        }
    }
}
