namespace FutureValue.WebApi.DTO
{
    public class ErrorDto
    {
        public ErrorDto(string error, string message)
        {
            Error = error;
            Message = message;
        }

        public string Error { get; set; }
        public string Message { get;set; }
    }
}
