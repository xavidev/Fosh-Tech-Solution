using System;

namespace Sat.Recruitment.Api.Models.Users
{
    public abstract class WelcomeGiftCalculationStrategy
    {
        public static WelcomeGiftCalculationStrategy For(string userType)
        {
            return userType switch
            {
                "Normal" => new NormalUserGiftCalculationStrategy(),
                "SuperUser" => new SuperUserGiftCalculationStrategy(),
                "Premium" => new PremiumUserGiftCalculationStrategy(),
                _ => new NullGiftCalculationStrategy()
            };
        }

        public abstract decimal Calculate(decimal initialMoney);
    }

    public class NullGiftCalculationStrategy : WelcomeGiftCalculationStrategy
    {
        public override decimal Calculate(decimal initialMoney)
        {
            return 0;
        }
    }

    public class PremiumUserGiftCalculationStrategy : WelcomeGiftCalculationStrategy
    {
        public override decimal Calculate(decimal initialMoney)
        {
            if (initialMoney <= 100) return 0;

            return initialMoney * 2;
        }
    }

    public class SuperUserGiftCalculationStrategy : WelcomeGiftCalculationStrategy
    {
        public override decimal Calculate(decimal initialMoney)
        {
            if (initialMoney <= 100) return 0;

            return initialMoney * Convert.ToDecimal(0.20);
        }
    }

    public class NormalUserGiftCalculationStrategy : WelcomeGiftCalculationStrategy
    {
        public override decimal Calculate(decimal initialMoney)
        {
            if (initialMoney == 100 || initialMoney == 10) return 0;

            if (initialMoney > 100)
            {
                return initialMoney * Convert.ToDecimal(0.12);
            }

            return initialMoney * Convert.ToDecimal(0.8);
        }
    }
}