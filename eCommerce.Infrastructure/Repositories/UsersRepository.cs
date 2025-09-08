//using Dapper;
//using eCommerce.Core.DTO;
//using eCommerce.Core.Entities;
//using eCommerce.Core.RepositoryContracts;
//using eCommerce.Infrastructure.EfDbContext;

//namespace eCommerce.Infrastructure.Repositories;

//internal class UsersRepository : IUsersRepository
//{
//  private readonly DbContext _dbContext;

//  public UsersRepository(DapperDbContext dbContext)
//  {
//    _dbContext = dbContext;
//  }

//  public async Task<ApplicationUser?> AddUser(ApplicationUser user)
//  {
//    //Generate a new unique user ID for the user
//    user.UserID = Guid.NewGuid();

//    // SQL Query to insert user data into the "Users" table.
//    string query = "INSERT INTO public.\"Users\"(\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") VALUES(@UserID, @Email, @PersonName, @Gender, @Password)";
//    int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

//    if (rowCountAffected > 0 )
//    {
//      return user;
//    }
//    else
//    {
//      return null;
//    }
//  }

//  public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
//  {
//    //SQL query to select a user by Email and Password
//    string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password";
//    var parameters = new { Email = email, Password = password };

//    ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

//    return user;
//  }
//}

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
        // Use LINQ to find a user matching email and password
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }


    public async Task<ApplicationUser?> GetUserById(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
    }



}

