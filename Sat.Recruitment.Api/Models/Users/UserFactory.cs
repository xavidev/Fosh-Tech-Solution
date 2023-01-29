using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models.Users
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserRepository repository;

        public UserFactory(IUserRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<User> New(string name, string email, string address, string phone, string userType,
            decimal initialMoney)
        {
            var newUser = new User(name,
                email,
                address,
                phone,
                userType,
                initialMoney);
            
            var users = await repository.GetAll();
            if (IsDuplicated(users.ToList(), newUser))
                throw new UserDuplicatedException();

            return newUser;
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