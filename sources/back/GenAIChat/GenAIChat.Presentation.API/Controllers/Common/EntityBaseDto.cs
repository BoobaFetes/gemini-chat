﻿using GenAIChat.Domain.Common;

namespace GenAIChat.Presentation.API.Controllers.Common
{
    public class EntityBaseDto : IEntityDomain
    {
        public int Id { get; set; }
    }

}