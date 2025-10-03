namespace SLAProjectHub.Communication.Responses
{
    public class ResponseLoginJson
    {


        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
