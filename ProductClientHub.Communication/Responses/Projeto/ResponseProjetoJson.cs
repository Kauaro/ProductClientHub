namespace SLAProjectHub.Communication.Responses.Projeto
{
    public class ResponseProjetoJson
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string Aluno { get; set; } = string.Empty;
        public string UsuarioNome { get; set; } = string.Empty;
    }
}
