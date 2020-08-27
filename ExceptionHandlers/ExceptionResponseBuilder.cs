using Microsoft.AspNetCore.Http;
using System;

namespace Customer.API.ExceptionHandlers
{
    public static class ExceptionResponseBuilder
    {
        public static ErrorResponseModel createRespone(Exception exception, HttpContext context)
        {
            var errorMessage = exception == null ?
                                "Unknown Error"
                                : exception.InnerException?.Message ?? exception.Message;

            var model = new ErrorResponseModel
            {
                ErrorId = Guid.NewGuid().ToString(),
                ErrorMessage = errorMessage,
                ErrorStatusCode = context.Response.StatusCode,

            };

            return model;
        }
    }
}
