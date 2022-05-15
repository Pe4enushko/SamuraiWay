using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDbWork.Models
{
    public class ChallengeModel
    {
        public int id {get;set;} = 0;
        public string Name {get;set;} = "";
        public string Description { get; set; } = "";

        public ChallengeModel(int id, string name, string description)
        {
            this.id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}
