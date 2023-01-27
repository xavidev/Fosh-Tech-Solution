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
            Email = email;
            Address = address;
            Phone = phone;
        }
    }
}