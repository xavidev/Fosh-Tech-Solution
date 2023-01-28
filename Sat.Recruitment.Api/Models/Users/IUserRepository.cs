using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }
}