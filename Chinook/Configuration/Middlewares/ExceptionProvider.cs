namespace Chinook.Configuration.Middlewares
{
    public record ExceptionProvider
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ExceptionProvider(int status, string message, string stackTrace)
        {
            Status = status;
            Message = message;
            StackTrace = stackTrace;
        }

        public ExceptionProvider(int status, string message)
        {
            Status = status;
            Message = message;
        }

    }
}
