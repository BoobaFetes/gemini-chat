﻿using GenAIChat.Domain.Document;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace GenAIChat.Infrastructure.Api.Gemini.Converter
{
    internal class UploadFileContentConverter
    {
        public static async Task<DocumentMetadataDomain> Convert(HttpContent responseContent)
        {
            var result = await responseContent.ReadFromJsonAsync<UploadFilContentResponse>()
                ?? throw new InvalidOperationException("Failed to deserialize the response from the Gemini API in charge to upload documents");

            return result.File;
        }

        private class UploadFilContentResponse
        {
            [Required]
            public DocumentMetadataDomain File { get; set; }
        }
    }
}