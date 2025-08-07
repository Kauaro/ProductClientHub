using System.Net;

namespace ProductClientHub.Exceptions.ExceptionBase
{
    public class ErrorOnValidationException : ProductClientHubException
    {

        private readonly List<string> _errors;
        public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErrors() => _errors;

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
    }
}
