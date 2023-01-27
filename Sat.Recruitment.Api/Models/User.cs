using System;
using System.Text;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        public string Name { get; }
        public string Email { get; set; }
        public string Address { get; }
        public string Phone { get;  }
        public string UserType { get; }
        public decimal Money { get; }
        
        public User(string name, string email, string address, string phone, string userType, decimal initialMoney)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = initialMoney + CalculateWelcomeGift(initialMoney);
        }

        private decimal CalculateWelcomeGift(decimal initialMoney)
        {
            return WelcomeGiftCalculationStrategy.For(UserType).Calculate(initialMoney);
        }
    }
}