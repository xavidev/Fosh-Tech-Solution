using System;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        public User()
        {
            
        }

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

        public void SetMoney(decimal money)
        {
            //I've done research about the term 'gif' in the context of betting,
            //as I didn't discover nothing about it I'll treat 'gif' as a typo
            //and I'll use gift :').
            switch (UserType)
            {
                case "Normal":
                {
                    if (money > 100)
                    {
                        var gif = money * Convert.ToDecimal(0.12);
                        Money += gif;
                    }

                    if (money < 100)
                    {
                        if (money > 10)
                        {
                            var gif = money * Convert.ToDecimal(0.8);
                            Money += gif;
                        }
                    }

                    break;
                }
                case "SuperUser":
                {
                    if (money > 100)
                    {
                        var gif = money * Convert.ToDecimal(0.20);
                        Money += gif;
                    }

                    break;
                }
                case "Premium":
                {
                    if (money > 100)
                    {
                        var gif = money * 2;
                        Money += gif;
                    }

                    break;
                }
            }
        }
    }
}