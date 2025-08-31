namespace EccomerceDemo.Application.User.Interfaces;

public interface IUserRepository
{
    Task<Domain.Entities.User.User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Domain.Entities.User.User?> GetByIdAsync(Domain.Entities.User.ValueObjects.UserId id, CancellationToken cancellationToken);
    Task AddAsync(Domain.Entities.User.User user, CancellationToken cancellationToken);
    Task<bool> GetExistsByEmailAsync(string email, CancellationToken cancellationToken);
}
