﻿using GenAIChat.Domain.Document;
using GenAIChat.Domain.Project;
using GenAIChat.Domain.Project.Group;
using GenAIChat.Domain.Project.Group.UserStory;
using GenAIChat.Domain.Project.Group.UserStory.Task;
using GenAIChat.Domain.Project.Group.UserStory.Task.Cost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenAIChat.Infrastructure.Database.Sqlite
{
    public class GenAiDbContext(DbContextOptions<GenAiDbContext> options) : DbContext(options)
    {
        public TDomain? Detach<TDomain>(TDomain? entity) where TDomain : class
        {
            if (entity == null) return null;

            // check status
            var entry = Entry(entity);
            if (entry.State == EntityState.Detached) return entity;

            // detache recursively
            entry.State = EntityState.Detached;

            foreach (var navigationEntry in entry.Navigations.Where(e => e.CurrentValue != null))
            {
                switch (navigationEntry)
                {
                    case CollectionEntry collectionEntry:
                        foreach (var relatedEntity in collectionEntry.CurrentValue!)
                            Detach(relatedEntity);
                        break;
                    default:
                        Detach(navigationEntry.CurrentValue);
                        break;
                }
            }

            return entity;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nom des tables
            modelBuilder.Entity<ProjectDomain>().ToTable("Projects");
            modelBuilder.Entity<DocumentDomain>().ToTable("Documents");
            modelBuilder.Entity<DocumentMetadataDomain>().ToTable("DocumentMetadatas");
            modelBuilder.Entity<UserStoryGroupDomain>().ToTable("UserStoryGroups");
            modelBuilder.Entity<UserStoryDomain>().ToTable("UserStories");
            modelBuilder.Entity<TaskDomain>().ToTable("Tasks");
            modelBuilder.Entity<TaskCostDomain>().ToTable("TaskCosts");
            modelBuilder.Entity<UserStoryRequestDomain>().ToTable("UserStoryRequests");

            // Relation ProjectDomain -> DocumentDomain
            modelBuilder.Entity<ProjectDomain>()
                .HasMany(p => p.Documents)
                .WithOne()
                .HasForeignKey(d => d.ProjectId);

            // Relation ProjectDomain -> UserStoryGroupDomain
            modelBuilder.Entity<ProjectDomain>()
                .HasMany(p => p.Groups)
                .WithOne()
                .HasForeignKey(ug => ug.ProjectId);

            // Relation DocumentDomain -> DocumentMetadataDomain
            modelBuilder.Entity<DocumentDomain>()
                .HasOne(d => d.Metadata)
                .WithOne()
                .HasForeignKey<DocumentMetadataDomain>(m => m.DocumentId);

            // Relation UserStoryGroupDomain -> UserStoryRequestDomain
            modelBuilder.Entity<UserStoryGroupDomain>()
                .HasOne(ug => ug.Request)
                .WithOne()
                .HasForeignKey<UserStoryRequestDomain>(p => p.GroupId);

            // Relation UserStoryGroupDomain -> UserStoryDomain
            modelBuilder.Entity<UserStoryGroupDomain>()
                .HasMany(ug => ug.UserStories)
                .WithOne()
                .HasForeignKey(us => us.GroupId);

            // Relation UserStoryDomain -> TaskDomain
            modelBuilder.Entity<UserStoryDomain>()
                .HasMany(us => us.Tasks)
                .WithOne()
                .HasForeignKey(t => t.UserStoryId);

            // Relation TaskDomain -> TaskCostDomain
            modelBuilder.Entity<TaskDomain>()
                .HasMany(t => t.WorkingCosts)
                .WithOne()
                .HasForeignKey(tc => tc.TaskId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
