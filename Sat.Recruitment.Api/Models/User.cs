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
        public decimal Money { get; private set; }
        
        public User(string name, string email, string address, string phone, string userType, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
            SetMoney(money);
        }

        private void SetMoney(decimal initialMoney)
        {
            //I've done research about the term 'gif' in the context of betting,
            //as I didn't discover nothing about it I'll treat 'gif' as a typo
            //and I'll use gift :').

            Money += CalculateWelcomeGift(initialMoney);
        }

        private decimal CalculateWelcomeGift(decimal initialMoney)
        {
            return WelcomeGiftCalculationStrategy.For(UserType).Calculate(initialMoney);
        }
    }
}