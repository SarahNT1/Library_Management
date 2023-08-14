using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
	public class Book
	{
		public int Id_book { get; set; }
		public int Id_publisher { get; set; }
		public int Id_category { get; set; }
		public string Isbn { get; set; }
		public string Title { get; set; }
		public DateTime PublishDate { get; set; }
		public int Quantity { get; set; }
		public int Is_Available { get; set; }

		public Book() { }
		public Book(int id, int id_p, int id_c, string isbn, string title, DateTime pubDate, int quantity, int is_Available)
		{
			Id_book = id;
			Id_publisher = id_p;
			Id_category = id_c;
			Isbn = isbn;
			Title = title;
			PublishDate = pubDate;
			Quantity = quantity;
			Is_Available = is_Available;
		}
	}

	
}
