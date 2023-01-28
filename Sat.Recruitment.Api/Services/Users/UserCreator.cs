using System.Collections.Generic;
using System.IO;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Services.Users
{
    public sealed class UserCreator
    {
        public void CreateUser(string name, string email, string address, string phone, string userType,
            decimal initialMoney)
        {
            var newUser = new User(name,
                email,
                address,
                phone,
                userType,
                initialMoney);

            List<User> users = GetAllUsers();
            if (IsDuplicated(users, newUser))
                throw new UserDuplicatedException();
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

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
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