using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Persistence
{
    public class CSVFileUserRepository : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAll()
        {
            List<User> users = new List<User>();

            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
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
    }
}