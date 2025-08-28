using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;
using EccomerceDemo.Domain.Primitives;

namespace EccomerceDemo.Domain.Entities;

public sealed class Client : Entity
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;

    private Client() : base() { }

    public static Result<Client> TryCreate(Guid id, string fullName, string email, Address address, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        if (address is null)
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        return new Client
        {
            Id = id,
            FullName = fullName,
            Email = email,
            Address = address,
            PhoneNumber = phoneNumber
        };
    }

    public static Result<Client> TryCreate(string fullName, string email, Address address, string phoneNumber)
    {
        return TryCreate(Guid.NewGuid(), fullName, email, address, phoneNumber);
    }
    
    public Result<Client> Update(string fullName, string email, Address address, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        if (address is null ||
            string.IsNullOrWhiteSpace(address.Street) ||
            string.IsNullOrWhiteSpace(address.City) ||
            string.IsNullOrWhiteSpace(address.ZipCode))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Client>(DomainError.Client.InvalidParameter);

        this.FullName = fullName;
        this.Email = email;
        this.Address = address;
        this.PhoneNumber = phoneNumber;

        return Result.Success(this);
    }
}
