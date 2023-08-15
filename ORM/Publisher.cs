using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Publisher
    {

        public int Id_publisher { get; set; }
        public string Name_p { get; set; }
        public string Phone_p { get; set; }
        public string Email_p { get; set; }

        public Publisher() { }
        public Publisher(int id, string name, string phone, string email)
        {
            Id_publisher = id;
            Name_p = name;
            Phone_p = phone;
            Email_p = email;
            
        }


    }
}

