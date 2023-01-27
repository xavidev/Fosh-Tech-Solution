using Bogus;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Test.Unit.Users
{
    public static class UserMother
    {
        public static User Normal(decimal initialMoney)
        {
            var user = new Faker<User>()
                .RuleFor(x => x.Address, f => f.Address.Direction())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Phone, f => f.Person.Phone)
                .RuleFor(x => x.UserType, "Normal")
                .RuleFor(x => x.Money, initialMoney);

            return user;
        }
    }
}