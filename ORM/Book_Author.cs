using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
	public class Book_Author
	{
		public int Id_author { get; set; }
		public int Id_book { get; set; }

		public Book_Author() { }
		public Book_Author(int id_author, int id_book)
		{
			Id_author = id_author;
			Id_book = id_book;
		}	
	}
}
