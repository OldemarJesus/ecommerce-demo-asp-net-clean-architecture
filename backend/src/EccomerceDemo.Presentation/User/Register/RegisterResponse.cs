namespace EccomerceDemo.Presentation.User.Register;

public class RegisterResponse
{
    public UserId UserId { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

public record UserId(string Value);
