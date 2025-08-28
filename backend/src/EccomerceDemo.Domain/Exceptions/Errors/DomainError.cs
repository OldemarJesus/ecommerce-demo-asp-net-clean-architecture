using EccomerceDemo.Domain.Abstractions;

namespace EccomerceDemo.Domain.Exceptions.Errors;

public static class DomainError
{
    public static class Client
    {
        public static readonly Error NotFound = new Error("NotFound", "The requested client resource was not found.");
        public static readonly Error InvalidParameter = new Error("InvalidParameter", "The provided client parameter is invalid.");
    }

    public static class Order
    {
        public static readonly Error NotFound = new Error("NotFound", "The requested order resource was not found.");
        public static readonly Error InvalidParameter = new Error("InvalidParameter", "The provided order parameter is invalid.");
    }

    public static class Item
    {
        public static readonly Error NotFound = new Error("NotFound", "The requested item resource was not found.");
        public static readonly Error InvalidParameter = new Error("InvalidParameter", "The provided item parameter is invalid.");
    }
}
