using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

class ExceptionFilter(ILogger<ExceptionFilter> logger) : IExceptionFilter
{

    public readonly ILogger<ExceptionFilter> _logger = logger;

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, message: "Exception");
        if(context.Exception is AlreadyExistsException) {
            HandleAlreadyExistsException(context);
        } else if(context.Exception is NotFoundException) {
            HandleNotFoundException(context);
        } else {
            HandleInternalServerError(context);
        }
    }

    private void HandleAlreadyExistsException(ExceptionContext context)
    {
            var statusCode = StatusCodes.Status409Conflict;

        context.Result = new JsonResult(new
        {
            StatusCode = statusCode,
            context.Exception.Message
        });

        context.HttpContext.Response.StatusCode = statusCode;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
            var statusCode = StatusCodes.Status404NotFound;

        context.Result = new JsonResult(new
        {
            StatusCode = statusCode,
            context.Exception.Message
        });

        context.HttpContext.Response.StatusCode = statusCode;
    }

    private void HandleInternalServerError(ExceptionContext context)
    {
        var statusCodeResult = StatusCodes.Status500InternalServerError;

        context.Result = new JsonResult(new
        {
            StatusCode = statusCodeResult,
            Message = "[capturado pelo filtro] Erro interno do servidor, tente novamente mais tarde"
        });

        context.HttpContext.Response.StatusCode = statusCodeResult;
    }

}