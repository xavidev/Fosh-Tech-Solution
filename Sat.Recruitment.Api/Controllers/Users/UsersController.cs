using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Services.Users;

namespace Sat.Recruitment.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("create-user")]
        public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserRequest request)
        {
            var userCreator = new UserCreator();
            try
            {
                userCreator.CreateUser(request.Name,
                    request.Email,
                    request.Address,
                    request.Phone,
                    request.UserType,
                    decimal.Parse(request.Money));
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