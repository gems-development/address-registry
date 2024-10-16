﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Gems.AddressRegistry.DataAccess;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataAccessServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services) =>
           services
            .AddSingleton<IAppDbContextFactory, AppDbContextFactory>(serviceProvider =>
            {
                var connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
                return new AppDbContextFactory(connectionString = null!);
            });

    }

}
