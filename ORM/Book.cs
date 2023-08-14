using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Book
    {
        //fields
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public DateTime DatePublished { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

        //constructor
        public Book(int bookId, int publisherId, int categoryId, string iSBN, string title, DateTime datePublished, int quantity, bool isAvailable)
        {
            BookId = bookId;
            PublisherId = publisherId;
            CategoryId = categoryId;
            ISBN = iSBN;
            Title = title;
            DatePublished = datePublished;
            Quantity = quantity;
            IsAvailable = isAvailable;
        }
    }
}
