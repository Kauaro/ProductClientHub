namespace SLAProjectHub.API.Entities
{
    public class Avaliacao : EntityBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Nota { get; set; } = 0.0;
        public Guid ProjetoId { get; set; }
        public Guid AlunoMatricula { get; set; }
    }
}
