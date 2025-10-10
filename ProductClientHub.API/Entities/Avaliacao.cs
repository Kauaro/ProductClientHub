namespace SLAProjectHub.API.Entities
{
    public class Avaliacao : EntityBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Nota { get; set; } = 0;

        public Projeto Projeto { get; set; } = null!;
        public string ProjetoCodigo { get; set; } = string.Empty;

        public Aluno Aluno { get; set; } = null!;
        public string AlunoMatricula { get; set; } = string.Empty;
    }
}
