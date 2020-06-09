// Copyright 2020 SoftSentre Contributors
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
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.CQRS.Commands;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.CQRS.Results;
using SoftSentre.Shoppingendly.Services.Products.Extensions;
using SoftSentre.Shoppingendly.Services.Products.Infrastructure.EntityFramework;

namespace SoftSentre.Shoppingendly.Services.Products.Infrastructure.CQRS.Commands
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IUnitOfWork unitOfWork)
        {
            _decorated = decorated.IfEmptyThenThrowAndReturnValue();
            _unitOfWork = unitOfWork.IfEmptyThenThrowAndReturnValue();
        }

        public async Task<ICommandResult> SendAsync(TCommand command)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync();

            var result = await _decorated.SendAsync(command);

            await _unitOfWork.CommitTransactionAsync(transaction);

            return result;
        }
    }
}