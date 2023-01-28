using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Users;

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
            var newUser = new User(request.Name,
                request.Email,
                request.Address,
                request.Phone,
                request.UserType,
                decimal.Parse(request.Money)
            );

            List<User> users = GetAllUsers();
            if (IsDuplicated(users, newUser))
                return new CreateUserResponse()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };


            return new CreateUserResponse()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
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

        private List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User(
                    line.Split(',')[0],
                    line.Split(',')[1],
                    line.Split(',')[3],
                    line.Split(',')[2],
                    line.Split(',')[4],
                    decimal.Parse((string) line.Split(',')[5])
                );

                users.Add(user);
            }

            reader.Close();

            return users;
        }

        private static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] {'@'}, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] {aux[0], aux[1]});
        }
    }
}