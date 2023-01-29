using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task Save(User user);
        Task Delete(string email);
    }
}