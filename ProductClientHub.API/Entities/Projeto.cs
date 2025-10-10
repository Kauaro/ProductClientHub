namespace SLAProjectHub.API.Entities
{
    public class Projeto : EntityBase
    {
        
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string Aluno { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;


        public Usuario Usuario { get; set; } = null!;
        public Guid UsuarioId { get; set; }


        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

    }
}
