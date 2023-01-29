using Sat.Recruitment.Api.Controllers.Users;

namespace Sat.Recruitment.Test.Acceptance.Controllers.Users
{
    public static class CreateUserRequestMother
    {
        public static CreateUserRequest NotCreated()
        {
            return new CreateUserRequest("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124m);
        }

        public static CreateUserRequest ExistentUser()
        {
            return new CreateUserRequest("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal",
                124m);
        }

        public static CreateUserRequest Invalid()
        {
            return new CreateUserRequest(null, null, null, null, null, 0);
        }
    }
}