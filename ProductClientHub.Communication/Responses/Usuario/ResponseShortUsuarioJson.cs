namespace SLAProjectHub.Communication.Responses.Usuario
{
    public class ResponseShortUsuarioJson
    {
        public Guid Id { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NivelAcesso { get; set; } = string.Empty;

    }
}
