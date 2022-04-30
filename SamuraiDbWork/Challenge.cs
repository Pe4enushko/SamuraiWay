using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDbWork
{
    public class Challenge
    {
        public int id {get;set;} = 0;
        public string Name {get;set;} = "";
        public string Description { get; set; } = "";

        public Challenge(int id, string name, string description)
        {
            this.id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}
