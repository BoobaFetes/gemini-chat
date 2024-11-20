﻿using GenAIChat.Application.Adapter.Api;
using GenAIChat.Application.Adapter.Database;
using GenAIChat.Application.Command.Common;
using GenAIChat.Domain.Document;
using MediatR;

namespace GenAIChat.Application.Command.Document
{
    public class UpdateDocumentCommandHandler(IGenAiApiAdapter genAiAdapter, IGenAiUnitOfWorkAdapter unitOfWork) : IRequestHandler<UpdateCommand<DocumentDomain>, DocumentDomain?>
    {
        public async Task<DocumentDomain?> Handle(UpdateCommand<DocumentDomain> request, CancellationToken cancellationToken)
        {
            var document = await unitOfWork.Documents.GetByIdAsync(request.Entity.Id);
            if (document is null) return null;

            document.ProjectId = request.Entity.ProjectId;
            document.Name = request.Entity.Name;
            document.Content = request.Entity.Content;
            document.Metadata = request.Entity.Metadata;

            // upload files to the GenAI
            await genAiAdapter.SendFilesAsync([document]);

            await unitOfWork.Documents.UpdateAsync(document);

            return document;
        }
    }

}
