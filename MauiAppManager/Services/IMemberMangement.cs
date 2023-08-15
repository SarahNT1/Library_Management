using ORM;
using Database;


namespace MauiAppManager.Services
{
	public interface IMemberManagement
	{
		Task<bool> AddMemberInfo(Member member);
	}
}
