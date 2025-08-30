using EccomerceDemo.Domain.Abstractions;

namespace EccomerceDemo.Domain.Exceptions.Errors;

public static class DomainError
{
    public static class Generic
    {
        public static readonly Error NotFound = new Error("NotFound", "The requested resource was not found.");
        public static readonly Error InvalidParameter = new Error("InvalidParameter", "The provided parameter is invalid.");
    }
}
