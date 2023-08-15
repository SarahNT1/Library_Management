using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ORM;
using Database;

namespace MauiAppManager.Services
{
	public interface IBookManagement
	{
		Task<bool> AddBookInfo(string name_p, string name_category, string name_author, string title, string isbn, int quantity, int selectedYear, int selectedMonth, int selectedDay);
		
	}
}
