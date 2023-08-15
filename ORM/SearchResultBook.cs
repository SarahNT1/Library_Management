using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
	public class SearchResultBook
	{
		public int Id_book { get; set; }
		public int Id_category { get; set; }
		public int Id_author { get; set; }
		public string Title { get; set; }

		//public string Author { get; set; }

		public List<string> Authors { get; set; }
		public string Category { get; set; }

		public int Quantity { get; set; }

		public string CombinedAuthors 
		{
			get
			{
				if (Authors != null && Authors.Any())
				{
					return string.Join(", ", Authors);
				}
				return "";
			}
		}

		public bool IsEditing { get; set; }
		public string EditedCategory { get; set; }
		public int EditedQuantity { get; set; }

		public string Editedtitle { get; set; }
		public int EditedAuthor { get; set; }


	}
}

