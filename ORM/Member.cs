using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Member
    {
        //fields
        public int MemberId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public bool IsActive { get; set; }

        //constructor
        public Member(int memberId, string username, string password, string firstName, string lastName, string phoneNumber, string email, string street, string city, string province, bool isActive)
        {
            MemberId = memberId;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Street = street;
            City = city;
            Province = province;
            IsActive = isActive;
        }

        public override string ToString()
        {
            return $"{MemberId}, {Username}, {Password}, {FirstName} {LastName}, {PhoneNumber}, {Email}, {Street}, {City}, {Province}";
        }
    }
}
