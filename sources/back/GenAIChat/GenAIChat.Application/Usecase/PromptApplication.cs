﻿using GenAIChat.Application.Adapter.Api;
using GenAIChat.Domain.Prompt;

namespace GenAIChat.Application.Usecase
{
    public interface IPromptApplication
    {
        Task<string> SendAsync(string prompt);
    }

    public class PromptApplication : IPromptApplication
    {
        private readonly IGenAiApiAdapter _genAiAdapter;

        public PromptApplication(IGenAiApiAdapter genAiAdapter)
        {
            _genAiAdapter = genAiAdapter;
        }

        public async Task<string> SendAsync(string prompt)
        {
            throw new Exception("Not implemented"); 
            // return await _genAiAdapter.SendRequestAsync(prompt);
        }
    }
}
