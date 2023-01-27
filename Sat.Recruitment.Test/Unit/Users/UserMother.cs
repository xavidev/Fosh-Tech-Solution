using Bogus;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Test.Unit.Users
{
    public static class UserMother
    {
        public static User Normal(decimal initialMoney)
        {
            return new Faker<User>()
                .CustomInstantiator(f => 
                    new User(
                        f.Person.FirstName,
                        f.Person.Email,
                        f.Address.Direction(),
                        f.Person.Phone,
                        "Normal",
                        initialMoney));
        }

        public static User Super(decimal initialMoney)
        {
            return new Faker<User>()
                .CustomInstantiator(f => 
                    new User(
                        f.Person.FirstName,
                        f.Person.Email,
                        f.Address.Direction(),
                        f.Person.Phone,
                        "SuperUser",
                        initialMoney));
        }

        public static User Premium(decimal initialMoney)
        {
            return new Faker<User>()
                .CustomInstantiator(f => 
                    new User(
                        f.Person.FirstName,
                        f.Person.Email,
                        f.Address.Direction(),
                        f.Person.Phone,
                        "Premium",
                        initialMoney));
        }
    }
}