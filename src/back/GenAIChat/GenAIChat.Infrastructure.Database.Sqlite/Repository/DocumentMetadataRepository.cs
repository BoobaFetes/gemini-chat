﻿using AutoMapper;
using GenAIChat.Domain.Document;
using GenAIChat.Infrastructure.Database.Sqlite.Repository.Generic;

namespace GenAIChat.Infrastructure.Database.Sqlite.Repository
{
    public class DocumentMetadataRepository(GenAiDbContext dbContext, IMapper mapper) : GenericRepository<DocumentMetadataDomain>(dbContext, mapper)
    {
    }
}
