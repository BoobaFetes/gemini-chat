﻿using GenAIChat.Application.Adapter.Database;
using GenAIChat.Domain.Common;
using MediatR;

namespace GenAIChat.Application.Command.Common
{
    public class CreateCommand<TDomain> : IRequest<TDomain> where TDomain : class, IEntityDomain, ICloneable
    {
        public required TDomain Entity { get; init; }
    }

    public class GetCreateCommandHandler<TDomain>(IRepositoryAdapter<TDomain> repository) : IRequestHandler<CreateCommand<TDomain>, TDomain> where TDomain : class, IEntityDomain, ICloneable
    {
        public async Task<TDomain> Handle(CreateCommand<TDomain> request, CancellationToken cancellationToken)
        {
            return await repository.AddAsync((TDomain)request.Entity.Clone());
        }
    }
}
