namespace Customer.API.ExceptionHandlers
{
    public class ErrorResponseModel
    {
        public string ErrorId { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorStatusCode { get; set; }
    }
}
