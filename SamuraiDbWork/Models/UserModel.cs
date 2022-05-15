using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDbWork.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
