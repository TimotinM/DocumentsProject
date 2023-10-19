using Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : BaseCommandResponse

    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting request {typeof(TRequest).Name} {DateTime.Now}");

            var result = await next();

            if (!result.Success)
            {
                _logger.LogError($"Failed request {typeof(TRequest).Name} {DateTime.Now}");
            }

            _logger.LogInformation($"Completed request {typeof(TRequest).Name} {DateTime.Now}");

            return result;
        }
    }
}
