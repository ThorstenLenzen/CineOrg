using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.Utilities.Cqrs.AspNetCore.Commands
{
    public class GenericCreatedAtRouteCommandHandler<TCommand, TQueryResult> : ControllerCommandHandlerBase<TCommand> where TCommand : ICommand
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly ILogger _logger;
        private readonly CommandHandlerOptions<TQueryResult> _options;

        public GenericCreatedAtRouteCommandHandler(
            ICommandProcessor commandProcessor, 
            ILogger<GenericCreatedAtRouteCommandHandler<TCommand, TQueryResult>> logger,
            CommandHandlerOptions<TQueryResult> options)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options;
        }
        
        public override async Task<IActionResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
    
            var model = await _commandProcessor.ProcessAsync(command, cancellationToken);

            _logger.LogDebug($"{GetType().Name} leaving.");
            
            var id = GetId(model!);
            
            object? serviceModel = null;
            if (_options?.ConversionFunction != null)
                serviceModel = _options.ConversionFunction(model!);

            return CreatedAtRoute(_options!.RouteName, new {id = id}, serviceModel ?? model!);
        }

        private Guid? GetId(object instance)
        {
            var body = _options.IdProperty.Body;

            if (body is not UnaryExpression expression)
            {
                throw new ArgumentException("IdProperty must be of type a property of type System.Guid.");
            }

            var idExpression = expression.Operand as MemberExpression;
            var idPropertyName = idExpression.Member.Name;

            var idPropertyInfo = typeof(TQueryResult).GetProperty(idPropertyName);
            var idValue = idPropertyInfo.GetValue(instance);

            return idValue as Guid?;
        }
    }
}