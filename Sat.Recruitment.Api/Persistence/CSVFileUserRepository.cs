using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Persistence
{
    public class CSVFileUserRepository : IUserRepository
    {
        private readonly string basePath;
        private const int NAME = 0;
        private const int EMAIL = 1;
        private const int ADDRESS = 3;
        private const int PHONE = 2;
        private const int USER_TYPE = 4;
        private const int INITIAL_MONEY = 5;

        public CSVFileUserRepository(string basePath)
        {
            this.basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            List<User> users = new List<User>();

            StreamReader reader = MakeFileReader();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                if(string.IsNullOrEmpty(line)) continue;
                
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

        public async Task Save(User user)
        {
            StreamWriter writer = MakeFileWriter();
            var line = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
            await writer.WriteLineAsync();
            await writer.WriteLineAsync(line);
            writer.Close();
        }

        public async Task Delete(string email)
        {
            //This obviously not scale :)
            
            var users = await this.GetAll();
            var userToDelete = users.SingleOrDefault(x => x.Email == email);
            if (userToDelete == null)
            {
                await Task.CompletedTask;
            }
            
            File.Delete(Path.Combine(basePath, "Users.txt"));
            var writer = MakeFileWriter();
            foreach (var user in users)
            {
                if(user.Email == userToDelete.Email) continue;
                
                var line = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
                await writer.WriteLineAsync(line);
            }
            writer.Close();
        }

        private StreamWriter MakeFileWriter()
        {
            var path = Path.Combine(basePath, "Users.txt");
            var fileStream = new FileStream(path, FileMode.Append);
            var writer = new StreamWriter(fileStream);

            return writer;
        }

        private StreamReader MakeFileReader()
        {
            var path = Path.Combine(basePath, "Users.txt");
            var fileStream = new FileStream(path, FileMode.Open);
            var reader = new StreamReader(fileStream);

            return reader;
        }
    }
}