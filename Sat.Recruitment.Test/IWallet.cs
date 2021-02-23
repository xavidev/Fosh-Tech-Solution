using System.Collections.Generic;

namespace Sat.Recruitment.Test
{
    public interface IWallet
    {
        decimal Money { get; set; }
        List<Money> Opearations { get; set; }
        void AddMoney(Money money);
    }
}