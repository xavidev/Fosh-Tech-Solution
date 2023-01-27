using System;

namespace Sat.Recruitment.Api.Models
{
    public abstract class WelcomeGiftCalculationStrategy
    {
        public static WelcomeGiftCalculationStrategy For(string userType)
        {
            switch (userType)
            {
                case "Normal":
                {
                    return new NormalUserGiftCalculationStrategy();
                }
                case "SuperUser":
                {
                    return new SuperUserGiftCalculationStrategy();
                }
                case "Premium":
                {
                    return new PremiumUserGiftCalculationStrategy();
                }
            }

            return new NullGiftCalculationStrategy();
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
            if (initialMoney > 100)
            {
                return initialMoney * 2;
            }

            return 0;
        }
    }

    public class SuperUserGiftCalculationStrategy : WelcomeGiftCalculationStrategy
    {
        public override decimal Calculate(decimal initialMoney)
        {
            if (initialMoney > 100)
            {
                return initialMoney * Convert.ToDecimal(0.20);
            }

            return 0;
        }
    }

    public class NormalUserGiftCalculationStrategy : WelcomeGiftCalculationStrategy
    {
        public override decimal Calculate(decimal initialMoney)
        {
            if (initialMoney > 100)
            {
                return initialMoney * Convert.ToDecimal(0.12);
            }

            if (initialMoney < 100)
            {
                if (initialMoney > 10)
                {
                    return initialMoney * Convert.ToDecimal(0.8);
                }
            }

            return 0;
        }
    }
}