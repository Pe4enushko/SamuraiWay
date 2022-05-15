using SamuraiDbWork.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamuraiDbWork.GlobalDB
{
    public class UserManager : BasicCommands
    {
        public UserModel GetUserById(int id)
        {
            DataTable DT = GetData("SELECT * FROM [Users] WHERE [id] = " + id);
            return new UserModel() {
                Name = DT.Rows[0].Field<string>("name"),
                Login = DT.Rows[0].Field<string>("login"),
                ProfilePicture = DT.Rows[0].Field<byte[]>("profilePicture")
            };
        }
        public int GetIdByLogin(string login)
        {
            return GetData($"SELECT [id] FROM [Users] WHERE [login] = '{login}'").Rows[0].Field<int>("id");
        }
        public bool RegisterUser(string login, string pass, string name, byte[] picture = null)
        {
            SqlParameter[] prms = new SqlParameter[] 
            {
                new SqlParameter("@plogin",login),
                new SqlParameter("@ppass",pass),
                new SqlParameter("@pname",name),
                new SqlParameter("@picture",picture)
            };
            if (SendData("Users",new string[] {"login","pass","name","profilePicture"},prms))
            {
                MessageBox.Show("Регистрация прошла успешно!");
                return true;
            }
            else
            {
                MessageBox.Show("Ошибка при регистрации(((99((9");
                return false;
            }
            
        }
        public bool CheckAuth(string login, string pass)
        {
            return GetData($"SELECT * FROM Users WHERE [login] = '{login}' AND [pass] = '{pass}'").Rows.Count > 0;
        }
        public bool SetCurrentChallenge(string login,string challengeName = "NULL",string challengeDesc = "NULL")
        {
            return SendData($"UPDATE [Users] SET" +
                $" [currentChallenge] = '{challengeName}'," +
                $"[challengeStartDate] = '{DateTime.Now.ToString("yyyy-MM-dd")}'," +
                $"[currentChallengeDesc] = N'{challengeDesc}'" +
                $" WHERE [login] = '{login}';");
        }
    }
}
