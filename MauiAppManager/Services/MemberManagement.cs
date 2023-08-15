using Database;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppManager.Services
{
	public class MemberManagement : IMemberManagement
	{
		public async Task<bool> AddMemberInfo(Member member)
		{
			bool success = await DBManager.AddMember(member);
			return success;

		}
	}
}