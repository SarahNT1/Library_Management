using System;

namespace ORM
{
    public class Author
    {
        //fields
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //relationships
        public List<BookAuthor> BookAuthorList { get; set; }

        //constructor
        public Author(int authorId, string firstName, string lastName)
        {
            AuthorId = authorId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}