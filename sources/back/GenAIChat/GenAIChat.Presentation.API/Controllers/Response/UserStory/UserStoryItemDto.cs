﻿namespace GenAIChat.Domain.Project
{
    public class UserStoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Cost { get; set; } = 0;
    }
}