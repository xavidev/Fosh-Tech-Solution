using System.Diagnostics;

namespace Sat.Recruitment.Api.Models.Users
{
    public class Email
    {
        private readonly string email;

        public Email(string email)
        {
            Debug.Assert(email != null, nameof(this.email) + " != null");
            this.email = email;
            EnsureMailIsValid(email);
        }

        private static void EnsureMailIsValid(string email)
        {
            //Domain specific logic for email validation
        }

        public string Value => email;
    }
}