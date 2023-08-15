namespace ORM
{
    public class Member
    {
        public int Id_member { get; set; }
        public string Login_m { get; set; }
        public string Password_m { get; set; }
        public string Fname_m { get; set; }
        public string Lname_m { get; set; }
        public string Phone_m { get; set; }
        public string Email_m { get; set; }
        public string Street_m { get; set; }
        public string City_m { get; set; }
        public string Province_m { get; set; }

        public Member()
        {

        }
        public Member(int id_member, string login_m, string password_m, string fname_m, string lname_m, string phone_m, string email_m, string street_m, string city_m, string province_m)
        {
            Id_member = id_member;
            Login_m = login_m;
            Password_m = password_m;
            Fname_m = fname_m;
            Lname_m = lname_m;
            Phone_m = phone_m;
            Email_m = email_m;
            Street_m = street_m;
            City_m = city_m;
            Province_m = province_m;
        }


    }
}
