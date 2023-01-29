using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models.Users
{
    public interface IUserFactory
    {
        Task<User> New(string name, string email, string address, string phone, string userType,
            decimal initialMoney);
    }
}