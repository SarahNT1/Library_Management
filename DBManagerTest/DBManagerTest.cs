
using Database;
using ORM;
using NUnit.Framework;

namespace DBManagerTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DBConnectTest()
        {
            Task<bool> expected = DBManager.Connect();
            Assert.IsTrue(expected.Result);
        }

        [Test]
        public void DBDisconnectTest()
        {
            Task<bool> expected = DBManager.Connect();
            Assert.IsTrue(expected.Result);

            bool isConnected = DBManager.IsConnected();
            Assert.IsTrue(isConnected);

            DBManager.Disconnect();
            isConnected = DBManager.IsConnected();
            Assert.IsFalse(isConnected);
        }

        [Test]
        public void GetOneMemberTest()
        {

            Task<bool> expected = DBManager.Connect();
            Assert.IsTrue(expected.Result);

            string inputId = "1"; 

            Member member = (Member)DBManager.GetOneMember(inputId);

            
            Assert.NotNull(member); 
            Assert.AreEqual(inputId, member.Id_member.ToString());

            DBManager.Disconnect();
        }

        [Test]
        public async Task GetCategoriesTest()
        {
			Task<bool> expected = DBManager.Connect();
			Assert.IsTrue(await expected);

			List<Category> list = await DBManager.GetCategories();
            Assert.NotNull(list);
		}

		[Test]
		public async Task GetPublisherTest()
		{
			Task<bool> expected = DBManager.Connect();
			Assert.IsTrue(await expected);

			List<Publisher> list = await DBManager.GetPublishers();
			Assert.NotNull(list);
		}

		[Test]
		public async Task GetAuthorsTest()
		{
			Task<bool> expected = DBManager.Connect();
			Assert.IsTrue(await expected);

			List<Author> list = await DBManager.GetAuthors();
			Assert.NotNull(list);
		}

		
	}
}