using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using ORM;

namespace database
{
    public class DBManagerAdminMembers
    {
        public List<Member> SearchMembers(MySqlConnection connection, string userColumnName, string userIdentifier)
        {
            string columnName = "CONCAT(`fname_m`, ' ', `lname_m`)";
            string identifier = $"%{userIdentifier}%";
            string equation = "LIKE";
            if(userColumnName == "MemberID")
            {
                columnName = "id_member";
                equation = "=";
                identifier = $"{userIdentifier}";
            }

            string setQuery = "SELECT * FROM member";
            string whereClause = $" WHERE {columnName} {equation} '{identifier}';";

            MySqlCommand command = new MySqlCommand(setQuery + whereClause, connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<Member> members = new List<Member>();

            while(reader.Read())
            {
                bool isActive = Convert.ToBoolean(reader.GetInt32(10));

                Member member = new Member(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), isActive);

                members.Add(member);
            }

            return members;
        }

        public bool DeleteMember(DBConnections connect, MySqlConnection connection, string memberId)
        {
            int rowIndex = 0;

            string sqlCheckLoan = $"SELECT * FROM loan JOIN member USING(id_member) WHERE id_member = {memberId};";
            string sqlCheckReserved = $"SELECT * FROM book_reserved JOIN member USING(id_member) WHERE id_member = {memberId};";

            MySqlCommand commandCheckLoan = new MySqlCommand(sqlCheckLoan, connection);
            MySqlDataReader readerCheckLoan = commandCheckLoan.ExecuteReader();

            while(readerCheckLoan.Read())
            {
                rowIndex++;
            }

            connect.CloseConnection(connection);

            connect.OpenConnection(connection);

            MySqlCommand commandCheckReserved = new MySqlCommand(sqlCheckReserved, connection);
            MySqlDataReader readerCheckReserved = commandCheckReserved.ExecuteReader();

            while(readerCheckReserved.Read())
            {
                rowIndex++;
            }

            connect.CloseConnection(connection);

            if(rowIndex == 0)
            {
                connect.OpenConnection(connection);

                string sqlDeleteQuery = $"DELETE FROM member WHERE id_member = {memberId};";

                MySqlCommand commandDelete = new MySqlCommand(sqlDeleteQuery, connection);

                commandDelete.ExecuteNonQuery();

                connect.CloseConnection(connection);

                return true;
            }
            return false;
        }
    }
}
