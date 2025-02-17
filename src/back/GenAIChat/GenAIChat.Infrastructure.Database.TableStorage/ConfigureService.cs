﻿using Azure.Data.Tables;
using GenAIChat.Application.Adapter.Database;
using GenAIChat.Domain.Document;
using GenAIChat.Domain.Project;
using GenAIChat.Domain.Project.Group;
using GenAIChat.Domain.Project.Group.UserStory;
using GenAIChat.Domain.Project.Group.UserStory.Task;
using GenAIChat.Domain.Project.Group.UserStory.Task.Cost;
using GenAIChat.Infrastructure.Database.TableStorage.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GenAIChat.Infrastructure.Database.TableStorage
{
    public static class ConfigureService
    {
        public static void AddGenAiChatInfrastructureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // database configuration
            var databaseProvider = configuration.GetValue<string>("DatabaseProvider") ?? throw new InvalidOperationException("The DatabaseProvider property is not set in the appsettings.json");
            var connectionString = configuration.GetConnectionString(databaseProvider) ?? throw new InvalidOperationException("The DatabaseProvider property's value is not found in the ConnectionStrings section");
            if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException("The connection string cannot be null or empty.");

            var service = new TableServiceClient(connectionString);
            services.AddSingleton(service);

            services.AddScoped<IRepositoryAdapter<ProjectDomain>, ProjectRepository>();
            services.AddScoped<IRepositoryAdapter<DocumentDomain>, DocumentRepository>();
            services.AddScoped<IRepositoryAdapter<DocumentMetadataDomain>, DocumentMetadataRepository>();
            services.AddScoped<IRepositoryAdapter<UserStoryGroupDomain>, UserStoryGroupRepository>();
            services.AddScoped<IRepositoryAdapter<UserStoryDomain>, UserStoryRepository>();
            services.AddScoped<IRepositoryAdapter<TaskDomain>, TaskRepository>();
            services.AddScoped<IRepositoryAdapter<TaskCostDomain>, TaskCostRepository>();
        }
    }
}
