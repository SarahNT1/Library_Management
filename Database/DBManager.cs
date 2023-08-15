using MySqlConnector;
using System;
using System.Diagnostics.Metrics;
using System.Data;
using ORM;
using System.IO;
using System.Numerics;
using System.Data.Common;
using System.Net;


namespace Database
{
	public class DBManager
	{
		private static MySqlConnection connection;
		/// <summary>
		/// Connect to the database
		/// </summary>
		/// <returns>True if it is connected</returns>
		public static async Task<bool> Connect()
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
			{
				Server = "localhost",
				Database = "cprg211",
				UserID = "root",
				Password = "0rb3n3tAngakongpassword!",
			};

			connection = new MySqlConnection(builder.ConnectionString);

			try
			{
				await connection.OpenAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}

		/// <summary>
		/// Disconnect to database
		/// </summary>
		public static void Disconnect()
		{
			if (connection != null && connection.State == ConnectionState.Open)
			{
				connection.Close();
			}
		}
		/// <summary>
		/// Check if DB connected with program
		/// </summary>
		/// <returns>True if it is connected</returns>
		public static bool IsConnected()
		{
			if (connection != null && connection.State == ConnectionState.Open)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Find all members in the member table
		/// </summary>
		/// <returns>Member list</returns>
		public static List<Member> GetAllMember()
		{
			List<Member> list = new List<Member>();

			if (!IsConnected())
			{
				return null;
			}

			string sql = "select * from member;";

			MySqlCommand command = new MySqlCommand(sql, connection);
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				string loginID = reader.GetString(1);
				string password = reader.GetString(2);
				string fname = reader.GetString(3);
				string lname = reader.GetString(4);
				string phone = reader.GetString(5);
				string email = reader.GetString(6);
				string street = reader.GetString(7);
				string city = reader.GetString(8);
				string province = reader.GetString(9);

				list.Add(new Member(id, loginID, password, fname, lname, phone, email, street, city, province));
			}

			return list;

		}
		
		/// <summary>
		/// Find member by table primary key(ID)
		/// </summary>
		/// <param name="inputId">Value to find</param>
		/// <returns>A member</returns>
		public static Object GetOneMember(string inputId)
		{
			string sql = "select * from member where id_member = " + inputId;
			Member member = null;
			MySqlCommand command = new MySqlCommand(sql, connection);
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				string loginID = reader.GetString(1);
				string password = reader.GetString(2);
				string fname = reader.GetString(3);
				string lname = reader.GetString(4);
				string phone = reader.GetString(5);
				string email = reader.GetString(6);
				string street = reader.GetString(7);
				string city = reader.GetString(8);
				string province = reader.GetString(9);

				member = new Member(id, loginID, password, fname, lname, phone, email, street, city, province);
			}

			return member;

		}

		/// <summary>
		/// Find member my login ID
		/// </summary>
		/// <param name="id_login">Value to find</param>
		/// <returns>A member</returns>
		public static Object FindMemberInfo(string id_login)
		{
			string sql = "select * from member where Login_m = '" + id_login + "'";
			Member member = null;
			MySqlCommand command = new MySqlCommand(sql, connection);
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				string loginID = reader.GetString(1);
				string password = reader.GetString(2);
				string fname = reader.GetString(3);
				string lname = reader.GetString(4);
				string phone = reader.GetString(5);
				string email = reader.GetString(6);
				string street = reader.GetString(7);
				string city = reader.GetString(8);
				string province = reader.GetString(9);

				member = new Member(id, loginID, password, fname, lname, phone, email, street, city, province);
			}

			return member;
		}

		/// <summary>
		/// Get a category list from the category table
		/// </summary>
		/// <returns>A list of category</returns>
		public static async Task<List<Category>> GetCategories()
		{
			bool connected = await Connect();
			if (!connected) { return null; }

			List<Category> list = new List<Category>();
			string sql = "select * from category";
			Category category = null;

			if (connected)
			{
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					using (MySqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							int id_category = reader.GetInt32(0);
							string name_category = reader.GetString(1);
							category = new Category(id_category, name_category);
							list.Add(category);
						}
					}
				}

				Disconnect();
			}

			return list;

		}

		/// <summary>
		/// Get publishser list from the publisher table
		/// </summary>
		/// <returns>A list of publisher</returns>
		public static async Task<List<Publisher>> GetPublishers()
		{
			List<Publisher> list = new List<Publisher>();
			string sql = "select * from publisher";
			Publisher publisher;

			if (await Connect())
			{
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					using (MySqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							int id_publisher = reader.GetInt32(0);
							string name_publisher = reader.GetString(1);
							string phone_publisher = reader.GetString(2);
							string email_publisher = reader.GetString(3);

							publisher = new Publisher(id_publisher, name_publisher, phone_publisher, email_publisher);
							list.Add(publisher);
						}
					}
				}
				Disconnect();
			}

			return list;
		}

		/// <summary>
		/// Get author list from the author table
		/// </summary>
		/// <returns>A list of author</returns>
		public static async Task<List<Author>> GetAuthors()
		{
			List<Author> list = new List<Author>();
			string sql = "select * from author";
			Author author = null;

			if (await Connect())
			{
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					using (MySqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							int id_author = reader.GetInt32(0);
							string fname_a = reader.GetString(1);
							string lname_a = reader.GetString(2);

							author = new Author(id_author, fname_a, lname_a);
							list.Add(author);
						}
					}
				}

				Disconnect();
			}

			return list;

		}

		/// <summary>
		/// Add a book to the book table
		/// </summary>
		/// <param name="book">Value to append</param>
		/// <returns>Primary key of the book</returns>
		public static async Task<int> InsertBook(Book book)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return -1;
			}
			string sql = "INSERT INTO book (id_publisher, id_category, isbn, title, published_date, quantity, is_available) " +
				 "VALUES (@IdPublisher, @IdCategory, @Isbn, @Title, @PublishedDate, @Quantity, 1); " +
				 "SELECT LAST_INSERT_ID();";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@IdPublisher", book.Id_publisher);
				command.Parameters.AddWithValue("@IdCategory", book.Id_category);
				command.Parameters.AddWithValue("@Isbn", book.Isbn);
				command.Parameters.AddWithValue("@Title", book.Title);
				command.Parameters.AddWithValue("@PublishedDate", book.PublishDate);
				command.Parameters.AddWithValue("@Quantity", book.Quantity);

				object result = await command.ExecuteScalarAsync();
				if (int.TryParse(result.ToString(), out int insertedId))
				{
					Disconnect();
					return insertedId; // Indicate failure
				}
				else
				{
					Disconnect();
					return -1; // Indicate failure
				}
			}
		}

		/// <summary>
		/// Add to the book_author table(bridge table between book and author)
		/// </summary>
		/// <param name="book_Author">Value to append.</param>
		/// <returns>True if the value added</returns>
		public static async Task<bool> InsertBookAuthor(Book_Author book_Author)
		{
			bool connected = await Connect();
			if (!connected) { return false; }
			string sql = "INSERT INTO book_author (id_author, id_book) VALUES (@id_author, @id_book);";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id_author", book_Author.Id_author);
				command.Parameters.AddWithValue("@id_book", book_Author.Id_book);

				int rowAffected = await command.ExecuteNonQueryAsync();
				Disconnect();
				return rowAffected > 0;
			}

		}

		/// <summary>
		/// Search book by ID, Title, or Author
		/// </summary>
		/// <param name="selectedSearchOption">Value to find</param>
		/// <param name="inputText">Value to find</param>
		/// <returns>A list of books.</returns>
		public static async Task<List<SearchResultBook>> SearchBooksBy(string selectedSearchOption, string inputText)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return null;
			}

			string sql = "SELECT b.id_book, b.id_category, ba.id_author, b.title, CONCAT(a.fname_a, ' ', a.lname_a) AS authors, c.name_category, b.quantity " +
						 "FROM book b " +
						 "JOIN book_author ba ON b.id_book = ba.id_book " +
						 "JOIN author a ON ba.id_author = a.id_author " +
						 "JOIN category c ON b.id_category = c.id_category ";

			string whereClause = string.Empty;

			if (selectedSearchOption == "Title")
			{
				whereClause = $"b.title LIKE @inputText";
			}
			else if (selectedSearchOption == "Author")
			{
				whereClause = $"a.fname_a LIKE @inputText OR a.lname_a LIKE @inputText";
			}
			else if (selectedSearchOption == "Category")
			{
				whereClause = $"c.name_category LIKE @inputText";
			}

			if (!string.IsNullOrEmpty(whereClause))
			{
				sql += $"WHERE {whereClause}";
			}

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@inputText", $"%{inputText}%");

				using (MySqlDataReader reader = await command.ExecuteReaderAsync())
				{

					List<SearchResultBook> searchResults = new List<SearchResultBook>();
					while (reader.Read())
					{
						SearchResultBook result = new SearchResultBook
						{
							Id_book = reader.GetInt32("id_book"),
							Id_category = reader.GetInt32("id_category"),
							Id_author = reader.GetInt32("id_author"),
							Title = reader.GetString("title"),
							//Author = reader.GetString("author"),
							Category = reader.GetString("name_category"),
							Quantity = reader.GetInt32("quantity")
						};

						string concatenatedAuthors = reader.GetString("authors");
						result.Authors = concatenatedAuthors.Split(',').Select(x => x.Trim()).ToList();
						searchResults.Add(result);
					}
					Disconnect();
					return searchResults;
				}
			}
		}

		/// <summary>
		/// Update the category of a book
		/// </summary>
		/// <param name="bookId">Value to find</param>
		/// <param name="newCategory">Value to change</param>
		/// <returns>Task<bool> if it changed successfully.</bool></returns>
		public static async Task<bool> UpdateBookCategory(int bookId, string newCategory)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			string sql = "UPDATE book SET id_category = (SELECT id_category FROM category WHERE name_category = @newCategory) WHERE id_book = @bookId";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@newCategory", newCategory);
				command.Parameters.AddWithValue("@bookId", bookId);

				int rowsAffected = await command.ExecuteNonQueryAsync();

				Disconnect();

				return rowsAffected > 0;
			}

		}

		/// <summary>
		/// Update the quantity of a book
		/// </summary>
		/// <param name="bookId">Value to find</param>
		/// <param name="newQuantity">Value to change</param>
		/// <returns>Task<bool> if it changed successfully.</returns>
		public static async Task<bool> UpdateBookQuantity(int bookId, int newQuantity)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			string sql = "UPDATE book SET quantity = @newQuantity WHERE id_book = @bookId";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@newQuantity", newQuantity);
				command.Parameters.AddWithValue("@bookId", bookId);

				int rowsAffected = await command.ExecuteNonQueryAsync();

				Disconnect();

				return rowsAffected > 0;
			}
		}

		/// <summary>
		/// Update the author of a book
		/// </summary>
		/// <param name="bookId">Value to Find</param>
		/// <param name="authorId">Value to changed</param>
		/// <returns>Task<bool> if it changed successfully.</returns>
		public static async Task<bool> UpdateBookAuthor(int bookId, int authorId)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			string sql = "UPDATE book_author SET id_author = @authorId WHERE id_book = @bookId";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@authorId", authorId);
				command.Parameters.AddWithValue("@bookId", bookId);

				int rowsAffected = await command.ExecuteNonQueryAsync();

				Disconnect();

				return rowsAffected > 0;
			}
		}

		/// <summary>
		/// Delete book from the book_author, loan, and book table
		/// </summary>
		/// <param name="bookId">Value to delete</param>
		/// <returns>Task<bool> if it delete successfully</returns>
		public static async Task<bool> DeleteBook(int bookId)
		{
			//Check bridging tables if the book is there or not
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			int rowsAffected;
			string sqlCheckReserveQuery = $"SELECT COUNT(*) FROM book_reserved JOIN book USING(id_book) WHERE id_book = @bookId;";

			using (MySqlCommand command = new MySqlCommand(sqlCheckReserveQuery, connection))
			{
				command.Parameters.AddWithValue("@bookId", bookId);

				object result = await command.ExecuteScalarAsync();

				rowsAffected = result != null ? Convert.ToInt32(result) : 0;
			}
			
			
			//Delete data
			if (rowsAffected == 0)
			{
				//Delete from bridging table(book_author) first then from book table

				string sqlDelABridgeQuery = $"DELETE FROM book_author WHERE id_book = @bookId;";
				string sqlDelLBridgeQuery = $"DELETE FROM loan WHERE id_book = @bookId;";
				string sqlDelBookQuery = $"DELETE FROM book WHERE id_book = @bookId;";

				MySqlCommand commandDelABridge = new MySqlCommand(sqlDelABridgeQuery, connection);
				MySqlCommand commandDelLBridge = new MySqlCommand(sqlDelLBridgeQuery, connection);
				MySqlCommand commandDelBook = new MySqlCommand(sqlDelBookQuery, connection);


				commandDelABridge.Parameters.AddWithValue("bookId", bookId );
				commandDelLBridge.Parameters.AddWithValue("bookId", bookId);
				commandDelBook.Parameters.AddWithValue("bookId", bookId);

				await commandDelABridge.ExecuteNonQueryAsync();
				await commandDelLBridge.ExecuteNonQueryAsync();
				await commandDelBook.ExecuteNonQueryAsync();

				Disconnect();

				return true;
			}
			else
			{
				Disconnect();
				return false;
			}
		}

		/// <summary>
		/// Check ID when a member register
		/// </summary>
		/// <param name="id">Value to find</param>
		/// <returns>Task<bool> if there is a memeber who has the same ID</returns>
		public static async Task<bool> IsIdDuplicated(string id)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			string sql = "SELECT COUNT(*) FROM Member WHERE login_m = @id";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", id);

				object result = await command.ExecuteScalarAsync();
				int count = Convert.ToInt32(result);

				Disconnect();

				return count > 0;
			}
		}

		/// <summary>
		/// Add a memeber to the member table
		/// </summary>
		/// <param name="member">Value to append</param>
		/// <returns>Task<bool> if it add membe successfully. </returns>
		public static async Task<bool> AddMember(Member member)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}
			string sql = "INSERT INTO member (login_m, password_m, fname_m, lname_m, phone_m, email_m, street_m, city_m, province_m, is_active) " +
						 "VALUES (@login, @password, @firstname, @lastname, @phone, @email, @street, @city, @province, 1)";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@login", member.Login_m);
				command.Parameters.AddWithValue("@password", member.Password_m);
				command.Parameters.AddWithValue("@firstname", member.Fname_m);
				command.Parameters.AddWithValue("@lastname", member.Lname_m);
				command.Parameters.AddWithValue("@phone", member.Phone_m);
				command.Parameters.AddWithValue("@email", member.Email_m);
				command.Parameters.AddWithValue("@street", member.Street_m);
				command.Parameters.AddWithValue("@city", member.City_m);
				command.Parameters.AddWithValue("@province", member.Province_m);

				int rowsAffected = await command.ExecuteNonQueryAsync();

				Disconnect();

				return rowsAffected > 0;

			}
		}

		/// <summary>
		/// Search a member by ID or name from member table.
		/// </summary>
		/// <param name="selectedSearchOption">Option values to find</param>
		/// <param name="inputText">Value to find</param>
		/// <returns>A list of member</returns>
		public static async Task<List<Member>> SearchMembersBy(string selectedSearchOption, string inputText)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return null;
			}

			string sql = "SELECT * " +
						 "FROM member m ";

			string whereClause = string.Empty;

			if (selectedSearchOption == "ID") //it's int in the db
			{
				whereClause = $"m.id_member = @inputText";
			}
			else if (selectedSearchOption == "Name")
			{
				whereClause = $"m.fname_m LIKE @inputText OR m.lname_m LIKE @inputText";
			}
			

			if (!string.IsNullOrEmpty(whereClause))
			{
				sql += $"WHERE {whereClause}";
			}

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				//command.Parameters.AddWithValue("@inputText", $"%{inputText}%");
				command.Parameters.AddWithValue("@inputText", selectedSearchOption == "ID" ? inputText : $"%{inputText}%");

				using (MySqlDataReader reader = await command.ExecuteReaderAsync())
				{

					List<Member> searchResults = new List<Member>();
					while (reader.Read())
					{
						Member result = new Member
						{
							Id_member = reader.GetInt32("id_member"),
							Login_m = reader.GetString("login_m"),
							Password_m = reader.GetString("password_m"),
							Fname_m = reader.GetString("fname_m"),
							Lname_m = reader.GetString("lname_m"),
							Phone_m = reader.GetString("phone_m"),
							Email_m = reader.GetString("email_m"),
							Street_m = reader.GetString("street_m"),
							City_m = reader.GetString("city_m"),
							Province_m = reader.GetString("province_m")
						};

						searchResults.Add(result);
					}
					Disconnect();
					return searchResults;
				}
			}
		}

		/// <summary>
		/// Update member information
		/// </summary>
		/// <param name="member">A member</param>
		/// <returns>True if it updated successfully.</returns>
		public static async Task<bool> UpdateMemberInfo(Member member)
		{
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			string sql = "UPDATE member SET quantity = @newQuantity WHERE id_book = @bookId";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
			//	command.Parameters.AddWithValue("@newQuantity", newQuantity);
			//	command.Parameters.AddWithValue("@bookId", bookId);

				int rowsAffected = await command.ExecuteNonQueryAsync();

			//	Disconnect();

				return rowsAffected > 0;
			}

		}

		/// <summary>
		/// Check book Availability
		/// </summary>
		/// <param name="bookId">The book Id</param>
		/// <returns>True if it is available.</returns>
		public static async Task<bool> CheckAvailability(int bookId)
		{
			try
			{
				bool connected = await Connect();
				if (!connected)
				{
					return false;
				}

				string sql = $"SELECT is_available FROM book WHERE id_book = {bookId};";

				bool available = false;

				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					using (MySqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							available = Convert.ToBoolean(reader.GetInt32(0));
						}
					}
				}

				Disconnect();

				return available;
			}
			catch(Exception ex)
			{
				return false;
			}
        }

		/// <summary>
		/// Adds a book_reserved row
		/// </summary>
		/// <param name="memberId">Members Id</param>
		/// <param name="bookId">The book Id</param>
		/// <returns>True if it added successfully.</returns>
		public static async Task<bool> AddReserved(string memberId, int bookId)
		{
			try
			{
				bool connected = await Connect();
				if (!connected)
				{
					return false;
	}

				string sql = $"INSERT INTO book_reserved(id_book, id_member, status_reserved) VALUES({bookId}, {memberId}, 0);";

				bool success = false;

				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					int rowsAffected = await command.ExecuteNonQueryAsync();

					success = rowsAffected > 0;
				}

				Disconnect();

				return success;
			}
			catch(Exception ex)
			{
				return false;
			}
        }

		/// <summary>
		/// Update book quantity
		/// </summary>
		/// <param name="bookId">The book Id</param>
		/// <returns>True if it updated successfully.</returns>
		public static async Task<bool> UpdateQuantityLoan(int bookId)
		{
			try
			{
				bool connected = await Connect();
				if (!connected)
				{
					return false;
				}
				//Get quantity - 1
				bool success = false;
				int quantity = 0;

				string sqlCurrent = $"SELECT quantity FROM book WHERE id_book = {bookId};";

				using (MySqlCommand commandCurrent = new MySqlCommand(sqlCurrent, connection))
				{
					using (MySqlDataReader reader = await commandCurrent.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							quantity = reader.GetInt32(0);
						}
						quantity--;
					}
				}

				Disconnect();

                //Update Quantity 
                connected = await Connect();
                if (!connected)
                {
                    return false;
                }

                string sql = $"UPDATE book SET quantity = {quantity} WHERE id_book = {bookId};";

				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					int rowsAffected = await command.ExecuteNonQueryAsync();

					success = rowsAffected > 0;
				}

                Disconnect();

                if (quantity == 0)
				{
                    connected = await Connect();
                    if (!connected)
                    {
                        return false;
                    }

					string sqlAvailability = $"UPDATE book SET is_available = 0 WHERE id_book = {bookId};";

					using(MySqlCommand command = new MySqlCommand( sqlAvailability, connection))
					{
						await command.ExecuteNonQueryAsync();
					}
                    Disconnect();
                }

				return success;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Adds loan
		/// </summary>
		/// <param name="memberId">Member's Id</param>
		/// <param name="bookId">The book Id</param>
		/// <returns>True if it updated successfully.</returns>
		public static async Task<bool> AddLoan(string memberId, int bookId)
		{
            bool connected = await Connect();
          	if (!connected)
          	{
              	return false;
            }

			DateTime currentDate = DateTime.Now;
			string startDate = currentDate.ToString("yyyy-MM-dd");

			string sql = $"INSERT INTO loan(id_book, id_member, start_date, return_date) VALUES({bookId}, {memberId}, '{startDate}', '{startDate}');";

			bool success = false;

			using(MySqlCommand command = new MySqlCommand(sql, connection))
			{
				int rowsAffected = await command.ExecuteNonQueryAsync();

				success = rowsAffected > 0;
			}

          	Disconnect();

           	return success;
        }

		/// <summary>
		/// Find books Id
		/// </summary>
		/// <param name="bookTitle">The book title</param>
		/// <returns>Book Id(int) if found.</returns>
		public static async Task<int> FindBookId(string bookTitle)
		{
			bool connected = await Connect();

			string sql = $"SELECT id_book FROM book WHERE title = '{bookTitle}';";

			int bookId = 0;

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				using (MySqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (reader.Read())
					{
						bookId = reader.GetInt32(0);
					}
				}

				Disconnect();

				return bookId;
			}
		}

		/// <summary>
		/// Update book quantity(+1)
		/// </summary>
		/// <param name="bookId">The book Id</param>
		/// <returns>True if it updated successfully.</returns>
		public static async Task<bool> UpdateQuantityReturn(int bookId)
		{
			try
			{
				bool connected = await Connect();
				if (!connected)
				{
					return false;
				}
				//Get quantity + 1
				bool success = false;
				int quantity = 0;

				string sqlCurrent = $"SELECT quantity FROM book WHERE id_book = {bookId};";

				using (MySqlCommand commandCurrent = new MySqlCommand(sqlCurrent, connection))
				{
					using (MySqlDataReader reader = await commandCurrent.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							quantity = reader.GetInt32(0);
						}
					}
				}

				Disconnect();

				if (quantity == 0)
				{
                    connected = await Connect();
                    if (!connected)
                    {
                        return false;
                    }

                    string sqlAvailability = $"UPDATE book SET is_available = 1 WHERE id_book = {bookId};";

                    using (MySqlCommand command = new MySqlCommand(sqlAvailability, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
					Disconnect();
                }

                //Update Quantity 
                connected = await Connect();
                if (!connected)
                {
                    return false;
                }

                string sql = $"UPDATE book SET quantity = {quantity + 1} WHERE id_book = {bookId};";

				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					int rowsAffected = await command.ExecuteNonQueryAsync();

					success = rowsAffected > 0;
				}

				Disconnect();

				return success;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Update book quantity
		/// </summary>
		/// <param name="bookId">The book Id</param>
		/// <param name="memberId">Member's Id</param>
		/// <returns>True if it updated successfully.</returns>
		public static async Task<bool> DeleteReserved(int bookId, string memberId)
        {
			bool connected = await Connect();
			if (!connected)
			{
				return false;
			}

			string sql = $"DELETE FROM book_reserved WHERE id_member = {memberId} AND id_book = {bookId};";

			bool success = false;

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				int rowsAffected = await command.ExecuteNonQueryAsync();

				success = rowsAffected > 0;
			}

			Disconnect();

			return success;
		}

		/// <summary>
		/// Update book quantity
		/// </summary>
		/// <param name="bookId">The book Id</param>
		/// <param name="memberId">Member's Id</param>
		/// <returns>True if it updated successfully.</returns>
		public static async Task<bool> UpdateLoan(int bookId, string memberId)
	    {
			bool connected = await Connect();
		    if (!connected)
	        {
                return false;
			}

		    DateTime currentTime = DateTime.Now;
	        string returnDate = currentTime.ToString("yyyy-MM-dd");

            string sql = $"UPDATE loan SET return_date = '{returnDate}' WHERE id_member = {memberId} AND id_book = {bookId};";

			bool success = false;

		    using (MySqlCommand command = new MySqlCommand(sql, connection))
	        {
                int rowsAffected = await command.ExecuteNonQueryAsync();

				success = rowsAffected > 0;
			}

		    Disconnect();

			return success;
	    }
		
		/// <summary>
		/// Update member information after editing. 
		/// Every change in once edit will be updated together.
		/// </summary>
		/// <param name="newPhone"></param>
		/// <param name="newEmail"></param>
		/// <param name="newStreet"></param>
		/// <param name="newCity"></param>
		/// <param name="newProv"></param>
		/// <param name="id_m"></param>
		/// <returns></returns>
    		public static async Task UpdateMember(string newPhone, string newEmail, string newStreet, string newCity, string newProv, int id_m)
		{
			bool connected = await Connect();
			if (connected)
			{
				string sql = "update member set phone_m = @newPhone, email_m = @newEmail, street_m = @newStreet, city_m = @newCity, province_m = @newProv where id_member = @user;";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@newPhone", newPhone);
					command.Parameters.AddWithValue("@newEmail", newEmail);
					command.Parameters.AddWithValue("@newStreet", newStreet);
					command.Parameters.AddWithValue("@newCity", newCity);
					command.Parameters.AddWithValue("@newProv", newProv);
					command.Parameters.AddWithValue("@user", id_m);
					command.ExecuteNonQuery();


					Disconnect();

				}
			}
			
		}
		/// <summary>
		/// The is method is for deleting member. 
		/// The member which is selected will be removed.
		/// </summary>
		/// <param name="id_m"></param>
		/// <returns></returns>
		public static async Task DeleteMember(int id_m)
		{
			bool connected = await Connect();
			if (connected)
			{
				string sql = "delete from member where id_member = @user;";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@user", id_m);
					command.ExecuteNonQuery();
					Disconnect();

				}
			}
		}
	}
}

