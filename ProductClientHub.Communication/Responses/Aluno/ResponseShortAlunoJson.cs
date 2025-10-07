namespace SLAProjectHub.Communication.Responses.Aluno
{
    public class ResponseShortAlunoJson
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
