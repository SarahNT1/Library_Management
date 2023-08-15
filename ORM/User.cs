using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class User
    {
        public int Id_member { get; set; }
        public string Login_m { get; set; }
        public string Password_m { get; set; }
        public string Fname_m { get; set; }
        public string Lname_m { get; set; }
        public string Phone_m { get; set; }
        public string Email_m { get; set; }
        public string Street_m { get; set; }
        public string City_m { get; set; }
        public string Province_m { get; set; }
    }
}
