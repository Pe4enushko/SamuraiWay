using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDbWork
{
    public class Challenges : BasicCommands
    {
        public Challenges(string dbpath) : base(dbpath)
        {
        }
        public List<Challenge> GetAllChallenges()
        {
            List<Challenge> result = new List<Challenge>();
            DataTable dt = GetData("SELECT * FROM Challenges");
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                result.Add(new Challenge
                    ((int)dt.Rows[row].ItemArray[0], (string)dt.Rows[row].ItemArray[1], (string)dt.Rows[row].ItemArray[2]));
            }
            return result;
        }
        public Challenge GetChallengeById(int id)
        {
            DataTable dt = GetData("SELECT * FROM Challenges WHERE [id] = " + id);
            return new Challenge(int.Parse((string)dt.Rows[0].ItemArray[0]), (string)dt.Rows[0].ItemArray[1], (string)dt.Rows[0].ItemArray[2]);
        }
        public string GetDescription(string name)
        {
            return GetData($"SELECT description FROM Challenges WHERE [name] = '{name}'").Rows[0].ItemArray[0].ToString();
        }
    }
}
