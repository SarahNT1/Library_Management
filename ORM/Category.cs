using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Category
    {
        //fields
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        //constructor
        public Category(int id, string name)
        {
            CategoryId = id;
            CategoryName = name;
        }
    }
}
