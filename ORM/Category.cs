using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
	public class Category
	{
		public int Id_category { get; set; }
		public string Name_category { get; set; }

		public Category() { }
		public Category(int id_category, string name_category)
		{
			Id_category = id_category;
			Name_category = name_category;
		}	
	}
}
