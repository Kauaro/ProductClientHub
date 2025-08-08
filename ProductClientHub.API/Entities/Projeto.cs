namespace ProductClientHub.API.Entities
{
    public class Projeto : EntityBase
    {
        
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string Aluno { get; set; } = string.Empty;
        public Guid UsuarioId { get; set; }
    }
}
