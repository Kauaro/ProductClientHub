namespace ProductClientHub.API.Entities
{
    public class Usuario : EntityBase
    {


        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public List<Projeto> Projeto { get; set; } = [];
    }
}
