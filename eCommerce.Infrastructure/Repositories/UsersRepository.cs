
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{
    private readonly EfDbContext _dbContext;

    public UsersRepository(EfDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        // Generate a new unique user ID
        user.UserID = Guid.NewGuid();

        // Add user entity to DbContext
        _dbContext.Users.Add(user);

        // Save changes to database
        int changes = await _dbContext.SaveChangesAsync();

        // Return the user if successfully added
        return changes > 0 ? user : null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
    
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }


    public async Task<ApplicationUser?> GetUserById(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
    }



}

