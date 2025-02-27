﻿using GenAIChat.Application.Adapter.Database;
using GenAIChat.Domain.Common;
using MediatR;

namespace GenAIChat.Application.Command.Common
{
    public class UpdateCommand<TDomain> : IRequest<bool?> where TDomain : class, IEntityDomain
    {
        public required TDomain Domain { get; init; }
    }

    public class UpdateCommandHandler<TDomain>(IRepositoryAdapter<TDomain> repository) : IRequestHandler<UpdateCommand<TDomain>, bool?> where TDomain : class, IEntityDomain
    {
        public async Task<bool?> Handle(UpdateCommand<TDomain> request, CancellationToken cancellationToken)
            => await repository.UpdateAsync(request.Domain);
    }
}
