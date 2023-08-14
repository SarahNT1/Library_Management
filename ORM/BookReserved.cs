using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class BookReserved
    {
        //fields
        public int ReserveId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public bool IsReserved { get; set; }

        //constructor
        public BookReserved(int reserveId, int bookId, int memberId, bool isReserved)
        {
            ReserveId = reserveId;
            BookId = bookId;
            MemberId = memberId;
            IsReserved = isReserved;
        }
    }
}
