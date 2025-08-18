//using eCommerce.Core.RepositoryContracts;
//using eCommerce.Infrastructure.DbContext;
//using eCommerce.Infrastructure.Repositories;
//using Microsoft.Extensions.DependencyInjection;

//namespace eCommerce.Infrastructure;

//public static class DependencyInjection
//{
//  /// <summary>
//  /// Extension method to add infrastructure services to the dependency injection container
//  /// </summary>
//  /// <param name="services"></param>
//  /// <returns></returns>
//  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
//  {
//    //TO DO: Add services to the IoC container
//    //Infrastructure services often include data access, caching and other low-level components.

//    services.AddTransient<IUsersRepository, UsersRepository>();

//    services.AddTransient<DapperDbContext>();

//    return services;
//  }
//}


using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Build a temporary service provider to get IConfiguration
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        // Now use configuration as before
        services.AddDbContext<EfDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

        services.AddTransient<IUsersRepository, UsersRepository>();

        return services;
    }

}
