﻿using GenAIChat.Application.Adapter.Database;
using GenAIChat.Application.Command.Common;
using GenAIChat.Domain.Common;
using GenAIChat.Domain.Project;
using MediatR;

namespace GenAIChat.Application.Command.Project
{
    public class ProjectCreateCommandHandler(IGenAiUnitOfWorkAdapter unitOfWork) : IRequestHandler<CreateCommand<ProjectDomain>, ProjectDomain>
    {
        public async Task<ProjectDomain> Handle(CreateCommand<ProjectDomain> request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Entity.Name)) throw new Exception("Name is required");

            var projects = await unitOfWork.Project.GetAllAsync(PaginationOptions.All);
            var projectWithSameName = await unitOfWork.Project.GetAllAsync(PaginationOptions.All, p => p.Name.ToLower().Equals(request.Entity.Name.ToLower()));
            if (projectWithSameName.Any()) throw new Exception("Name already exists");

            var item = new ProjectDomain(request.Entity) { Id = 0 };
            await unitOfWork.Project.AddAsync(item);

            return item;
        }
    }

}
