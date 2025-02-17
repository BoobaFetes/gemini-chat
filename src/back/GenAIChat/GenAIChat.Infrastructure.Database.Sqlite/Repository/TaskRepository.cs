﻿using GenAIChat.Domain.Project.Group.UserStory.Task;
using GenAIChat.Infrastructure.Database.Sqlite.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace GenAIChat.Infrastructure.Database.Sqlite.Repository
{
    public class TaskRepository(GenAiDbContext dbContext) : GenericRepository<TaskDomain>(dbContext)
    {
        protected override IQueryable<TaskDomain> GetProperties(IQueryable<TaskDomain> query) => query.Include(i => i.WorkingCosts);
    }
}
