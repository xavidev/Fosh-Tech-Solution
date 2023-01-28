namespace Sat.Recruitment.Api.Models.Users
{
    public class Personal
    {
        public string Name { get;}
        public string Email { get; }
        public string Address { get; }
        public string Phone { get; }
        
        public Personal(string name, string email, string address, string phone)
        {
            Name = name;
            Email = new Email(email).Value;
            Address = address;
            Phone = phone;
        }
    }
}