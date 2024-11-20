﻿using AutoMapper;
using GenAIChat.Application.Usecase;
using GenAIChat.Domain.Common;
using GenAIChat.Domain.Document;
using GenAIChat.Presentation.API.Controllers.Common;
using GenAIChat.Presentation.API.Controllers.Document;
using GenAIChat.Presentation.API.Controllers.Document.Request;
using Microsoft.AspNetCore.Mvc;

namespace GenAIChat.Presentation.API.Controllers
{
    [ApiController]
    [Route("project/{projectId}/[controller]")]
    public class DocumentController(DocumentApplication application, IMapper mapper)
        : ControllerBase
    {

        private static async Task<byte[]?> ReadFile(IFormFile file)
        {
            if (file is null || file.Length == 0) return null;

            // read file content
            byte[] buffer = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(buffer);
            return buffer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int projectId, [FromQuery] int offset = PaginationOptions.DefaultOffset, [FromQuery] int limit = PaginationOptions.DefaultLimit)
        {
            var options = new PaginationOptions(offset, limit);
            var result = await application.GetAllAsync(options, i => i.ProjectId == projectId);
            return Ok(mapper.Map<Paged<DocumentItemDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await application.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(mapper.Map<DocumentDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create(int projectId, [FromForm] DocumentRequest request)
        {
            // check
            if (!ModelState.IsValid) return BadRequest(new ErrorDto(ModelState));

            // action
            try
            {
                var content = await ReadFile(request.File);
                if (content is null) return BadRequest(new ErrorDto("File is empty or null"));

                var document = new DocumentDomain(request.File.FileName, request.File.ContentType, request.File.Length, content, projectId);

                // create document
                var result = await application.CreateAsync(document);

                return Created(string.Empty, mapper.Map<DocumentBaseDto>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto(ex));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int projectId, int id, [FromForm] DocumentRequest request)
        {
            // check
            if (!ModelState.IsValid) return BadRequest(new ErrorDto(ModelState));

            // action
            try
            {
                var content = await ReadFile(request.File);
                if (content is null) return BadRequest(new ErrorDto("File is empty or null"));

                var document = new DocumentDomain(id, request.File.FileName, request.File.ContentType, request.File.Length, content, projectId);

                // create document
                var result = await application.UpdateAsync(document);

                if (result is null) return NotFound();

                return Ok(mapper.Map<DocumentDto>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto(ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int projectId, int id)
        {
            var item = await application.GetByIdAsync(id);
            if (item?.ProjectId != projectId) return NotFound();

            var result = await application.DeleteAsync(id);
            return Ok(mapper.Map<DocumentDto>(result));
        }
    }
}
