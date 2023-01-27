namespace Sat.Recruitment.Api.Models.Users
{
    public class User
    {
        public string Name => this.personal.Name;

        public string Email
        {
            get => personal.Email;

            set => this.email = value;
        }

        public string Address => this.personal.Address;
        public string Phone => this.personal.Phone;
        public string UserType { get; }
        public decimal Money { get; }

        private Personal personal;
        private string email;
        
        public User(string name, string email, string address, string phone, string userType, decimal initialMoney)
        {
            this.personal = new Personal(name, email, address, phone);
            UserType = userType;
            Money = initialMoney + CalculateWelcomeGift(initialMoney);
        }

        private decimal CalculateWelcomeGift(decimal initialMoney)
        {
            return WelcomeGiftCalculationStrategy.For(UserType).Calculate(initialMoney);
        }
    }
}