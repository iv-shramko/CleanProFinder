using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;

namespace CleanProFinder.Server.Mediator.Pipeline
{
    public class ValidationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ServiceResponse
    {
        private readonly IValidationService _validationService;

        public ValidationBehaviour(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!IsResponseTypeServiceResponse())
            {
                return await next();
            }

            var validationResponse = await _validationService.ValidateAsync(request);
            if(validationResponse.IsSuccess)
            {
                return await next();
            }

            if(IsResponseNonGenericServiceResponse())
            {
                return (TResponse)validationResponse;
            }

            if (IsResponseGenericServiceResponse())
            {
                return (TResponse)MapGenericServiceResponse(validationResponse);
            }

            return await next();
        }

        private static bool IsResponseTypeServiceResponse()
        {
            if (IsResponseNonGenericServiceResponse())
            {
                return true;
            }

            if(IsResponseGenericServiceResponse())
            {
                return true;
            }

            return false;
        }

        private static bool IsResponseNonGenericServiceResponse()
        {
            return typeof(TResponse).Equals(typeof(ServiceResponse));
        }

        private static bool IsResponseGenericServiceResponse()
        {
            return typeof(TResponse).IsGenericType && 
                typeof(TResponse).GetGenericTypeDefinition().Equals(typeof(ServiceResponse<>));
        }

        private object MapGenericServiceResponse(ServiceResponse serviceResponse)
        {
            var responseResultType = typeof(TResponse).GenericTypeArguments.Single();

            var mapMethod = serviceResponse.GetType()
                .GetMethods()
                .First(m => m.Name.Equals(nameof(ServiceResponse.MapErrorResult)) &&
                    m.IsGenericMethod);

            var convertedGenericMethod = mapMethod.MakeGenericMethod(responseResultType);
            return convertedGenericMethod.Invoke(serviceResponse, null)!;
        }
    }
}
