﻿using GenAIChat.Domain.Common;
using System.Text;

namespace GenAIChat.Domain.Project.Group
{
    public class UserStoryPromptDomain : IEntityDomain
    {
        public int Id { get; set; }
        public string Context { get; set; } = string.Empty;
        public string Personas { get; set; } = string.Empty;
        public string Tasks { get; set; } = string.Empty;

        #region  navigation properties
        public int GroupId { get; set; }
        #endregion

        public UserStoryPromptDomain() { }
        public UserStoryPromptDomain(UserStoryPromptDomain domain)
        {
            Context = domain.Context;
            Personas = domain.Personas;
            Tasks = domain.Tasks;
        }

        public string ToGenAIRequest()
        {
            StringBuilder sb = new();
            void append(string key, string value)
            {
                sb.Append($"{key}:");
                sb.Append($"{value}{Environment.NewLine}");
            }

            append(nameof(Context), Context);
            append(nameof(Personas), Personas);
            append(nameof(Tasks), Tasks);

            return sb.ToString();
        }
    }
}
