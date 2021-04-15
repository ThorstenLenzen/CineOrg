using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Toto.Utilities.Exceptions;

namespace Toto.CineOrg.WebApi.Filters
{
    public class ValidateServiceModelFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = context
                .ModelState
                .Values
                .SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
                .ToList();

            var result = ErrorResult
                .Create("00009", "Validation Errors", errors);

            context.Result = new BadRequestObjectResult(result);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}    