﻿using System.Text.Json.Serialization;

namespace GenAIChat.Domain.Gemini.GeminiCommon
{
    public class GeminiContentPromptPart : IGeminiContentPart
    {
        [JsonPropertyName("txt")]
        public string Text { get; set; } = string.Empty;

        public GeminiContentPromptPart() { }

        public GeminiContentPromptPart(string prompt)
        {
            Text = prompt;
        }
    }
}