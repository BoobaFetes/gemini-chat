﻿using AutoMapper;
using GenAIChat.Application.Command.Common;
using GenAIChat.Domain.Common;
using GenAIChat.Domain.Project;
using GenAIChat.Presentation.API.Controllers.Common;
using GenAIChat.Presentation.API.Controllers.Dto;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GenAIChat.Presentation.API.Controllers
{
    [EnableCors(PolicyName = ConfigureService.SpaCors)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController(IMediator mediator, IMapper mapper)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, [FromQuery] int offset = PaginationOptions.DefaultOffset, [FromQuery] int limit = PaginationOptions.DefaultLimit)
        {
            var options = new PaginationOptions(offset, limit);
            var result = await mediator.Send(new GetAllPagedQuery<ProjectDomain>() { Options = options }, cancellationToken);
            return Ok(mapper.Map<Paged<ProjectBaseDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetByIdQuery<ProjectDomain>() { Id = id }, cancellationToken);
            if (result is null) return NotFound();
            return Ok(mapper.Map<ProjectDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectBaseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new CreateCommand<ProjectDomain>() { Domain = mapper.Map<ProjectDomain>(request) }, cancellationToken);
                return Created(string.Empty, mapper.Map<ProjectDto>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto(ex));
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] ProjectDto request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new UpdateCommand<ProjectDomain>() { Domain = mapper.Map<ProjectDomain>(request) }, cancellationToken);

                if (result is null) return NotFound();

                return Ok(mapper.Map<ProjectDto>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto(ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteByIdCommand<ProjectDomain>() { Id = id }, cancellationToken);
            if (result is null) return NotFound();
            return Ok(mapper.Map<ProjectDto>(result));
        }
    }
}
