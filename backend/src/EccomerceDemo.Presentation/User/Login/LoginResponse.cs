namespace EccomerceDemo.Presentation.User.Login;

public record class LoginResponse(Guid UserId, string Email, string FullName, string Token);