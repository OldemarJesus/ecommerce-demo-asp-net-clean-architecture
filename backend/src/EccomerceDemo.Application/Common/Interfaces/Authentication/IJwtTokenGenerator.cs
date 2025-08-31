namespace EccomerceDemo.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string fullName, string email);
}
