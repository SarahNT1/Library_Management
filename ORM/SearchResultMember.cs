using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
	public class SearchResultMember
	{
		public int Id_member { get; set; }
		public int Fname { get; set; }
		public int Lnae { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		
		public bool IsEditing { get; set; }
		public string EditedPhone { get; set; }
		public int EditedEmail { get; set; }

	}
}
