using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Responses;

namespace Journey.Api.filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException (ExceptionContext exceptionContext)
        {
            if(exceptionContext.Exception is JourneyException)   
            {
                var journeyEx = (JourneyException)exceptionContext.Exception; 

                exceptionContext.HttpContext.Response.StatusCode = (int)journeyEx.GetStatusCode();

                var responseJson = new ResponseErrorsJson(journeyEx.GetErrorMessage());

                exceptionContext.Result = new ObjectResult(responseJson);
            }
            else
            {
                exceptionContext.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrorsJson(new List<string>
                {
                    "Erro Desconhecido"
                });

                exceptionContext.Result = new ObjectResult(responseJson);
            }
        }
    }
}
