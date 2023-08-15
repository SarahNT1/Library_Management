using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ORM;

namespace MauiAppManager.Services
{
	public class BookManagement : IBookManagement
	{
		public async Task<bool> AddBookInfo(string name_p, string name_category, string name_author, string title, string isbn, int quantity, int selectedYear, int selectedMonth, int selectedDay)
		{
			try
			{
				var book = new Book();
				var book_author = new Book_Author();

				int publisherId = Convert.ToInt32(name_p);
				int categoryId = Convert.ToInt32(name_category);

				book.Id_publisher = publisherId;
				book.Id_category = categoryId;
				book.Isbn = isbn;
				book.Title = title;
				book.PublishDate = new DateTime(selectedYear, selectedMonth, selectedDay);
				book.Quantity = quantity;

				int insertedId = await DBManager.InsertBook(book);
				if (insertedId != -1)
				{
					if (name_author != null)
					{
						int authorId = Convert.ToInt32(name_author);
						book_author.Id_author = authorId;
						book_author.Id_book = insertedId;
						bool insertBookAuthor = await DBManager.InsertBookAuthor(book_author);

						return true;
					}
				}

				return false;
			}
			catch (Exception ex)
			{
				return false;
			}
		}


		public void BackToMainAdmin()
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public Task LogOut()
		{
			throw new NotImplementedException();
		}
	}
}
