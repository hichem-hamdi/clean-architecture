using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Register;
public static class UserErrorCodes
{
    public static class CreateUser
    {
        public const string MissingEmail = "EmailIsRequired";
        public const string InvalidEmail = "EmailIsInvalid";
        public const string FirstNameIsRequired = "FirstNameIsRequired";
        public const string LastNameIsRequired = "LastNameIsRequired";
        public const string PasswordIsRequired = "PasswordIsRequired";
        public const string PasswordTooShort = "PasswordTooShort";
    }
}
