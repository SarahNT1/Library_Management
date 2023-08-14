using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class BookAuthor
    {
        //fields
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        //constructor
        public BookAuthor(int authorId, int bookId)
        {
            AuthorId = authorId;
            BookId = bookId;
        }
    }
}
