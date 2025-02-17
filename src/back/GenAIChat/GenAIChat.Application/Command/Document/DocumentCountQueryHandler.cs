﻿using GenAIChat.Application.Adapter.Database;
using GenAIChat.Application.Command.Common;
using GenAIChat.Domain.Document;
using MediatR;

namespace GenAIChat.Application.Command.Document
{
    public class ProjectCountQueryHandler(IRepositoryAdapter<DocumentDomain> documentRepository) : IRequestHandler<CountQuery<DocumentDomain>, int>
    {
        public async Task<int> Handle(CountQuery<DocumentDomain> request, CancellationToken cancellationToken)
            => await documentRepository.CountAsync(request.Filter);
    }
}
