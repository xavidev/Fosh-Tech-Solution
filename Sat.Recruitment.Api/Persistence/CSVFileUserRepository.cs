using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Persistence
{
    public class CSVFileUserRepository : IUserRepository
    {
        private const int NAME = 0;
        private const int EMAIL = 1;
        private const int ADDRESS = 3;
        private const int PHONE = 2;
        private const int USER_TYPE = 4;
        private const int INITIAL_MONEY = 5;

        public async Task<IEnumerable<User>> GetAll()
        {
            List<User> users = new List<User>();

            var reader = MakeFileReader();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var splittedLine = line.Split(',');
                var user = new User(
                    splittedLine[NAME],
                    splittedLine[EMAIL],
                    splittedLine[ADDRESS],
                    splittedLine[PHONE],
                    splittedLine[USER_TYPE],
                    decimal.Parse(splittedLine[INITIAL_MONEY])
                );

                users.Add(user);
            }

            reader.Close();

            return users;
        }

        public Task Save(User user)
        {
            return Task.CompletedTask;
        }

        private static StreamReader MakeFileReader()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            var fileStream = new FileStream(path, FileMode.Open);
            var reader = new StreamReader(fileStream);

            return reader;
        }
    }
}