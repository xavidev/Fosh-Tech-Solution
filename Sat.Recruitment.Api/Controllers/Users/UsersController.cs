using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;

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
            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = decimal.Parse(request.Money)
            };

            SetMoneyValueToUser(decimal.Parse(request.Money), newUser);
            SetUserMail(newUser);

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
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse((string) line.Split(',')[5].ToString()),
                };
                users.Add(user);
            }

            reader.Close();

            return users;
        }

        private static void SetUserMail(User newUser)
        {
            var aux = newUser.Email.Split(new char[] {'@'}, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] {aux[0], aux[1]});
        }

        private static void SetMoneyValueToUser(decimal money, User newUser)
        {
            switch (newUser.UserType)
            {
                case "Normal":
                {
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }

                    if (money < 100)
                    {
                        if (money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = money * percentage;
                            newUser.Money = newUser.Money + gif;
                        }
                    }

                    break;
                }
                case "SuperUser":
                {
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }

                    break;
                }
                case "Premium":
                {
                    if (money > 100)
                    {
                        var gif = money * 2;
                        newUser.Money = newUser.Money + gif;
                    }

                    break;
                }
            }
        }
    }
}