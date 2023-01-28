using System;

namespace Sat.Recruitment.Api.Models.Users
{
    public class UserDuplicatedException : Exception
    {
        public UserDuplicatedException() : base("The user is duplicated")
        {
            
        }
    }
}