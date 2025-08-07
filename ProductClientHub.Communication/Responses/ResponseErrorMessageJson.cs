namespace ProductClientHub.Communication.Responses
{
    public class ResponseErrorMessageJson
    {
        public List<string> Errors { get; private set; }

        public ResponseErrorMessageJson(string message)
        {
            Errors = [message];
        }

        public ResponseErrorMessageJson(List<string> messages)
        {
            Errors = messages;
        }
    }
}
