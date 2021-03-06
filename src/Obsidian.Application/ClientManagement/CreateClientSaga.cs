﻿using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using System;
using System.Threading.Tasks;

namespace Obsidian.Application.ClientManagement
{
    public class CreateClientSaga : Saga, IStartsWith<CreateClientCommand, ClientCreationResult>
    {
        private bool _isCompleted;
        private readonly IClientRepository _repo;

        public CreateClientSaga(IClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClientCreationResult> StartAsync(CreateClientCommand command)
        {
            _isCompleted = true;
            var client = Client.Create(Guid.NewGuid(), command.RedirectUri);
            client.DisplayName = command.DisplayName;
            client.UpdateSecret();
            await _repo.AddAsync(client);
            return new ClientCreationResult
            {
                Succeed = true,
                Message = $"Client successfully created.",
                Id = client.Id
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;
    }
}