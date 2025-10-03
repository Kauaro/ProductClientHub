namespace ProductClientHub.Communication.Responses
{
    public class ResponseUsuarioJson
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string NivelAcesso { get; set; } = string.Empty;
        public List<ResponseShortProjetoJson> Projeto { get; set; } = [];
    }
}
