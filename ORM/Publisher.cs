using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Publisher
    {
        //fields
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        //constructor
        public Publisher(int publisherId, string publisherName, string phoneNumber, string email, string street, string city, string province)
        {
            PublisherId = publisherId;
            PublisherName = publisherName;
            PhoneNumber = phoneNumber;
            Email = email;
            Street = street;
            City = city;
            Province = province;
        }
    }
}
