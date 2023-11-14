namespace CleanProFinder.Shared.Errors.Base
{
    public class ServiceError : IDisplayError
    {
        public string Header { get; set; }
        public string ErrorMessage { get; set; }
        public int Code { get; set; }

        public ServiceError()
        {
            
        }

        public ServiceError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }
    }
}
