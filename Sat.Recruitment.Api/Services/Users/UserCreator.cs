using System.Threading.Tasks;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Services.Users
{
    public sealed class UserCreator
    {
        private readonly IUserFactory userFactory;
        private readonly IUserRepository userRepository;

        public UserCreator(IUserFactory userFactory, IUserRepository userRepository)
        {
            this.userFactory = userFactory;
            this.userRepository = userRepository;
        }
        public async Task CreateUser(string name, string email, string address, string phone, string userType,
            decimal initialMoney)
        {
            User newUser = await userFactory.New(name, email, address, phone, userType, initialMoney);
            await userRepository.Save(newUser);
        }
    }
}