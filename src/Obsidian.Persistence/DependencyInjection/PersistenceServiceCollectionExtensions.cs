﻿using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Obsidian.Domain.Repositories;
using Obsidian.Persistence.Repositories;

namespace Obsidian.Persistence.DependencyInjection
{
    public static class PersistenceServiceCollectionExtensions
    {
        /// <summary>
        /// Register repositories in <see cref="Repositories"/> as services in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services) =>
            services.AddScoped<IUserRepository, UserMongoRepository>()
                    .AddScoped<IClientRepository, ClientMongoRepository>()
                    .AddScoped(p => new MongoClient("mongodb://localhost:27017").GetDatabase("Obsidian"));
    }
}