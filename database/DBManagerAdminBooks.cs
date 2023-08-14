using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace database
{
    public class DBManagerAdminBooks
    {
        public List<string> SearchBook(MySqlConnection connection, string userColumnName, string identifier)
        {
            string columnName = "title";
            if(userColumnName == "Category")
            {
                columnName = "name_category";
            }
            else if(userColumnName == "Author")
            {
                columnName = "CONCAT(`fname_a`, ' ', `lname_a`)";
            }

            //Checks if the identifier contains single quotations then adds another single quotation after it
            if (identifier != null)
            {
                if (identifier.Contains("'"))
                {
                    int charIndex = identifier.IndexOf("'");
                    identifier = identifier.Insert(charIndex + 1, "'");
                }
            }

            string setQuery = "SELECT title, CONCAT(`fname_a`, ' ', `lname_a`), name_category FROM book JOIN category USING(id_category) JOIN book_author USING(id_book) JOIN author USING(id_author)";
            string whereClause = $" WHERE UPPER({columnName}) LIKE '%{identifier}%';";

            MySqlCommand command = new MySqlCommand(setQuery + whereClause, connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<string> bookList = new List<string>();

            while(reader.Read())
            {
                string title = reader.GetString(0);
                string author = reader.GetString(1);
                string category = reader.GetString(2);

                bookList.Add($"{title}, {author}, {category}");
            }

            return bookList;
        }

        public void AddBook(MySqlConnection connection)
        {
            //Add the book first then call AddBookRelationships to add it onto the bridging table.
            //No need to worry about the rest of the relationships.
        }

        public void AddBookRelationships(MySqlConnection connection)
        {

        }

        public bool DeleteBook(DBConnections connect, MySqlConnection connection, string title)
        {
            //Check bridging tables if the book is there or not
            if (title.Contains("'"))
            {
                int charIndex = title.IndexOf("'");
                title = title.Insert(charIndex + 1, "'");
            }

            string sqlCheckReserveQuery = $"SELECT * FROM book_reserved JOIN book USING(id_book) WHERE title = '{title}';";

            int rowIndex = 0;

            MySqlCommand commandCheckReserve = new MySqlCommand(sqlCheckReserveQuery, connection);
            MySqlDataReader readerReserve = commandCheckReserve.ExecuteReader();

            while (readerReserve.Read())
            {
                rowIndex++;
            }

            connect.CloseConnection(connection);

            //Delete data
            if (rowIndex == 0)
            {
                //Delete from bridging table(book_author) first then from book table
                connect.OpenConnection(connection);

                string sqlDelABridgeQuery = $"DELETE FROM book_author WHERE id_book IN(SELECT id_book FROM book WHERE title = '{title}');";
                string sqlDelLBridgeQuery = $"DELETE FROM loan WHERE id_book IN(SELECT id_book FROM book WHERE title = '{title}');";
                string sqlDelBookQuery = $"DELETE FROM book WHERE title = '{title}';";

                MySqlCommand commandDelABridge = new MySqlCommand(sqlDelABridgeQuery, connection);
                MySqlCommand commandDelLBridge = new MySqlCommand(sqlDelLBridgeQuery, connection);
                MySqlCommand commandDelBook = new MySqlCommand(sqlDelBookQuery, connection);

                commandDelABridge.ExecuteNonQuery();
                commandDelLBridge.ExecuteNonQuery();
                commandDelBook.ExecuteNonQuery();

                connect.CloseConnection(connection);

                return true;
            }
            else
            {
                return false;
            }
        }

        public string EditBook(MySqlConnection connection, string title, string newTitle, string author, string newAuthor, string category)
        {
            if(newAuthor != null)
            {
                string sqlAuthorQuery = $"UPDATE book_author SET id_author = {newAuthor} WHERE id_book = (SELECT id_book FROM book WHERE title = '{title}');";

                MySqlCommand commandAuthor = new MySqlCommand(sqlAuthorQuery, connection);

                commandAuthor.ExecuteNonQuery();
            }
            if(category != null)
            {
                string sqlCategoryQuery = $"UPDATE book SET id_category = {category} WHERE title = '{title}';";

                MySqlCommand commandCategory = new MySqlCommand(sqlCategoryQuery, connection);

                commandCategory.ExecuteNonQuery();
            }

            string sqlTitleQuery = $"UPDATE book SET title = '{newTitle}' WHERE title = '{title}';";

            MySqlCommand commandTitle = new MySqlCommand(sqlTitleQuery, connection);

            commandTitle.ExecuteNonQuery();

            return EditBookOutput(connection, newTitle);
        }

        public string EditBookOutput(MySqlConnection connection, string bookTitle)
        {
            string title = null;
            string author = null;
            string category = null;

            string setQuery = "SELECT title, CONCAT(`fname_a`, ' ', `lname_a`), name_category FROM book JOIN category USING(id_category) JOIN book_author USING(id_book) JOIN author USING(id_author)";
            string whereClause = $" WHERE title = '{bookTitle}';";

            MySqlCommand command = new MySqlCommand(setQuery + whereClause, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                title = reader.GetString(0);
                author = reader.GetString(1);
                category = reader.GetString(2);
            }

            return $"{title}, {author}, {category}";
        }

        public bool ReserveBookAdmin(DBConnections connect, MySqlConnection connection, string memberId, string bookTitle)
        {
            //Get ID
            connect.OpenConnection(connection);
            string bookId = "";
            string sqlGetBookIdQuery = $"SELECT id_book FROM book WHERE title = '{bookTitle}';";

            MySqlCommand commandGetBookId = new MySqlCommand(sqlGetBookIdQuery, connection);
            MySqlDataReader readerGetBookId = commandGetBookId.ExecuteReader();

            while(readerGetBookId.Read())
            {
                bookId = Convert.ToString(readerGetBookId.GetInt32(0));
            }

            connect.CloseConnection(connection);

            //Check if the book is available
            connect.OpenConnection(connection);
            int bookAvailable = 0;
            string sqlCheckAvailabilityQuery = $"SELECT is_available FROM book WHERE id_book = {bookId};";

            MySqlCommand commandAvailability = new MySqlCommand(sqlCheckAvailabilityQuery, connection);
            MySqlDataReader readerAvailability = commandAvailability.ExecuteReader();

            while(readerAvailability.Read())
            {
                bookAvailable = readerAvailability.GetInt32(0);
            }

            connect.CloseConnection(connection);

            if(bookAvailable == 1)
            {
                connect.OpenConnection(connection);

                //Add to reserved table then update is_available to 0
                string sqlAddToReserved = $"INSERT INTO book_reserved(id_book, id_member, status_reserved) VALUES({bookId}, {memberId}, 0);";

                MySqlCommand commandAddReserved = new MySqlCommand(sqlAddToReserved, connection);

                commandAddReserved.ExecuteNonQuery();
                connect.CloseConnection(connection);

                //Add to loan table (return_date will have temp. data)
                DateTime currentDate = DateTime.Now;
                string startDate = currentDate.ToString("yyyy-MM-dd");

                connect.OpenConnection(connection);
                string sqlAddLoanQuery = $"INSERT INTO loan(id_book, id_member, start_date, return_date) VALUES({bookId}, {memberId}, '{startDate}', '{startDate}');";

                MySqlCommand commandAddLoan = new MySqlCommand(sqlAddLoanQuery, connection);

                commandAddLoan.ExecuteNonQuery();
                connect.CloseConnection(connection);

                //Update book table is_available to 0
                connect.OpenConnection(connection);

                string sqlUpdateAvailability = $"UPDATE book SET is_available = 0 WHERE id_book = {bookId};";

                MySqlCommand commandUpdateAvail = new MySqlCommand(sqlUpdateAvailability, connection);

                commandUpdateAvail.ExecuteNonQuery();
                connect.CloseConnection(connection);

                return true;
            }
            return false;
        }

        public bool ReturnBookAdmin(DBConnections connect, MySqlConnection connection, string memberId, string bookTitle)
        {
            //Get book id
            string bookId = "";

            connect.OpenConnection(connection);
            string sqlGetBookIdQuery = $"SELECT id_book FROM book WHERE title = '{bookTitle}';";

            MySqlCommand commandGetBookId = new MySqlCommand(sqlGetBookIdQuery, connection);
            MySqlDataReader readerGetBookId = commandGetBookId.ExecuteReader();

            while(readerGetBookId.Read())
            {
                bookId = Convert.ToString(readerGetBookId.GetInt16(0));
            }

            connect.CloseConnection(connection);

            //Remove from book_reserved table
            connect.OpenConnection(connection);
            string sqlRemoveReservedQuery = $"DELETE FROM book_reserved WHERE id_book = {bookId} AND id_member = {memberId};";

            MySqlCommand commandRemoveReserved = new MySqlCommand(sqlRemoveReservedQuery, connection);
            commandRemoveReserved.ExecuteNonQuery();

            connect.CloseConnection(connection);

            //Update loan table to show return date
            DateTime currentDate = DateTime.Now;
            string returnDate = currentDate.ToString("yyyy-MM-dd");

            connect.OpenConnection(connection);
            string sqlUpdateLoanQuery = $"UPDATE loan SET return_date = '{returnDate}' WHERE id_book = {bookId} AND id_member = {memberId};";

            MySqlCommand commandUpdateLoan = new MySqlCommand(sqlUpdateLoanQuery, connection);

            commandUpdateLoan.ExecuteNonQuery();
            connect.CloseConnection(connection);

            //Update book availability
            connect.OpenConnection(connection);
            string sqlUpdateBookQuery = $"UPDATE book SET is_available = 1 WHERE id_book = {bookId};";

            MySqlCommand commandUpdateBook = new MySqlCommand(sqlUpdateBookQuery, connection);
            commandUpdateBook.ExecuteNonQuery();

            connect.CloseConnection(connection);

            return true;
        }
    }
}
