using EccomerceDemo.Domain.Abstractions;

namespace EccomerceDemo.Domain.Exceptions.Errors;

public static class DomainError
{
    public static class Generic
    {
        public static readonly Error NotFound = new Error("NotFound", "The requested resource was not found.");
        public static readonly Error InvalidParameter = new Error("InvalidParameter", "The provided parameter is invalid.");
        public static class User
        {
            public static readonly Error DuplicateEmail = new Error("DuplicateEmail", "The email address is already in use.");
            public static readonly Error InvalidCredentials = new Error("InvalidCredentials", "The provided credentials are invalid.");
        }
    }
}
