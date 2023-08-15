using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Author
    {
        public int Id_author { get; set; }
        public string Fname_a { get; set; }
        public string Lname_a { get; set; }
       

        public Author() { }
        public Author(int id, string name, string phone)
        {
            Id_author = id;
			Fname_a = name;
			Lname_a = phone;
           
        }
        

    }
}

