using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Controllers.Users
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        public string Money { get; set; }

        public CreateUserRequest()
        {
            
        }

        public CreateUserRequest(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
        }
    }
}