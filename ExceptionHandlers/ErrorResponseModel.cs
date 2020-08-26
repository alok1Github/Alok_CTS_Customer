using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Web.Mvc;

namespace Customer.API.ExceptionHandlers
{
    public class ErrorResponseModel
    {
        public string ErrorId { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorStatusCode { get; set; }

        //  public string Content { get; set; }
    }
}
