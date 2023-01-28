using System.Collections.Generic;
using System.Linq;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Services.Users
{
    public sealed class UserCreator
    {
        private readonly IUserRepository repository;

        public UserCreator(IUserRepository repository)
        {
            this.repository = repository;
        }
        public void CreateUser(string name, string email, string address, string phone, string userType,
            decimal initialMoney)
        {
            var newUser = new User(name,
                email,
                address,
                phone,
                userType,
                initialMoney);

            List<User> users = repository.GetAll().ToList();
            if (IsDuplicated(users, newUser))
                throw new UserDuplicatedException();
        }

        private static bool IsDuplicated(List<User> users, User newUser)
        {
            bool isDuplicated = false;
            foreach (var user in users)
            {
                if (user.Email == newUser.Email
                    ||
                    user.Phone == newUser.Phone)
                {
                    isDuplicated = true;
                }
                else if (user.Name == newUser.Name)
                {
                    if (user.Address == newUser.Address)
                    {
                        isDuplicated = true;
                    }
                }
            }

            return isDuplicated;
        }
    }
}