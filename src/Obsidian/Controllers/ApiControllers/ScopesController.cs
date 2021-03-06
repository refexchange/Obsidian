﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application;
using Obsidian.Application.Dto;
using Obsidian.Application.ScopeManagement;
using Obsidian.Authorization;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using Obsidian.Misc;
using System;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ScopesController : Controller
    {
        private readonly SagaBus _sagaBus;
        private readonly IPermissionScopeRepository _scopeRepository;

        public ScopesController(IPermissionScopeRepository scopeRepo, SagaBus bus)
        {
            _scopeRepository = scopeRepo;
            _sagaBus = bus;
        }

        [HttpGet]
        [RequireClaim(ManagementAPIClaimsType.IsScopeAcquirer, "Yes")]
        public async Task<IActionResult> Get()
        {
            var query = await _scopeRepository.QueryAllAsync();
            return Ok(query.ProjectTo<QueryModel.PermissionScope>());
        }

        [HttpGet("{id:guid}")]
        [RequireClaim(ManagementAPIClaimsType.IsScopeAcquirer, "Yes")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var scope = await _scopeRepository.FindByIdAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<QueryModel.PermissionScope>(scope));
        }

        [HttpPost]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsScopeCreator, "Yes")]
        public async Task<IActionResult> Post([FromBody]ScopeCreationDto dto)
        {
            var cmd = new CreateScopeCommand
            {
                Claims = dto.Claims,
                Description = dto.Description,
                DisplayName = dto.DisplayName,
                ScopeName = dto.ScopeName
            };

            var result = await _sagaBus.InvokeAsync<CreateScopeCommand, ScopeCreationResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = result.Id }), null);
            }
            return StatusCode(412, result.Message);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsScopeEditor, "Yes")]
        public async Task<IActionResult> Put([FromBody] UpdateScopeDto dto, Guid id)
        {
            var cmd = new UpdateScopeCommand
            {
                Id = id,
                Description = dto.Description,
                DisplayName = dto.DisplayName,
                Claims = dto.Claims
            };
            var result = await _sagaBus.InvokeAsync<UpdateScopeCommand, MessageResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest(result.Message);
        }
    }
}