namespace SLAProjectHub.Communication.Responses.Avaliacao
{
    public class ResponseAvaliacaoJson
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Nota { get; set; } = 0;
        public string AlunoMatricula { get; set; } = string.Empty;
        public string ProjetoCodigo { get; set; } = string.Empty;
        public string ProjetoNome { get; set; } = string.Empty;
    }
}
