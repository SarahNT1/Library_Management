using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Loan
    {
        //fields
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }

        //constructor
        public Loan(int loanId, int bookId, int memberId, DateTime startDate, DateTime returnDate)
        {
            LoanId = loanId;
            BookId = bookId;
            MemberId = memberId;
            StartDate = startDate;
            ReturnDate = returnDate;
        }
    }
}
