
using EccomerceDemo.Application.User.Interfaces;
using EccomerceDemo.Domain.Entities.User;
using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EcommerceDbContext _dbContext;

    public UserRepository(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<bool> GetExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}
