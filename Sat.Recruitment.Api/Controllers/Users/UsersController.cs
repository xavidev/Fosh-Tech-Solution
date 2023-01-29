using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Services.Users;

namespace Sat.Recruitment.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserCreator userCreator;

        public UsersController(UserCreator userCreator)
        {
            this.userCreator = userCreator;
        }
        [HttpPost]
        [Route("create-user")]
        public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                await userCreator.CreateUser(request.Name,
                    request.Email,
                    request.Address,
                    request.Phone,
                    request.UserType,
                    request.Money);
            }
            catch (UserDuplicatedException ex)
            {
                return new CreateUserResponse()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }
            
            return new CreateUserResponse()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }
    }
}