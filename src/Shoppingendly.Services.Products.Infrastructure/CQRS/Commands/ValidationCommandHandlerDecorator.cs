using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shoppingendly.Services.Products.Core.Extensions;
using Shoppingendly.Services.Products.Infrastructure.CQRS.Exceptions;
using Shoppingendly.Services.Products.Infrastructure.CQRS.Results;

namespace Shoppingendly.Services.Products.Infrastructure.CQRS.Commands
{
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly IList<IValidator<TCommand>> _validators;

        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IList<IValidator<TCommand>> validators)
        {
            _decorated = decorated.IfEmptyThenThrowAndReturnValue();
            _validators = validators.IfEmptyThenThrowAndReturnValue();
        }

        public async Task<ICommandResult> HandleAsync(TCommand command)
        {
            var errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!errors.Any())
                return await _decorated.HandleAsync(command);

            var errorBuilder = new StringBuilder();
            errorBuilder.AppendLine("Invalid command, reason: ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new InvalidCommandException(errorBuilder.ToString());
        }
    }
}

