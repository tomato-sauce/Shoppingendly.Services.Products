﻿// Copyright 2020 SoftSentre Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Threading.Tasks;
using Autofac;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.CQRS.Bus;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.CQRS.Commands;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.CQRS.Results;
using SoftSentre.Shoppingendly.Services.Products.Extensions;
using SoftSentre.Shoppingendly.Services.Products.Infrastructure.CQRS.Exceptions;

namespace SoftSentre.Shoppingendly.Services.Products.Infrastructure.CQRS.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly ILifetimeScope _lifetimeScope;

        public CommandBus(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope.IfEmptyThenThrowOrReturnValue();
        }

        public async Task<ICommandResult> SendAsync<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            await using var scope = _lifetimeScope.BeginLifetimeScope();
            var commandHandler = scope.ResolveOptional<ICommandHandler<TCommand>>();

            if (commandHandler == null)
            {
                throw new CommandPublishedFailedException(command);
            }

            return await commandHandler.SendAsync(command);
        }
    }
}