using System;
using System.Collections.Generic;
using System.Data;
using SamuraiDbWork.Models;

namespace SamuraiDbWork.LocalDB
{
    public class Challenges : BasicCommands
    {
        public List<ChallengeModel> GetAllChallenges()
        {
            List<ChallengeModel> result = new List<ChallengeModel>();
            DataTable dt = GetData("SELECT * FROM Challenges");
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                result.Add(new ChallengeModel
                    ((int)dt.Rows[row].ItemArray[0], (string)dt.Rows[row].ItemArray[1], (string)dt.Rows[row].ItemArray[2]));
            }
            return result;
        }
        public ChallengeModel GetChallengeById(int id)
        {
            DataTable dt = GetData("SELECT * FROM Challenges WHERE [id] = " + id);
            return new ChallengeModel(int.Parse((string)dt.Rows[0].ItemArray[0]), (string)dt.Rows[0].ItemArray[1], (string)dt.Rows[0].ItemArray[2]);
        }
        public string GetDescription(string name)
        {
            return GetData($"SELECT description FROM Challenges WHERE [name] = '{name}'").Rows[0].ItemArray[0].ToString();
        }
    }
}
