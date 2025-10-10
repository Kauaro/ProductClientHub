using ProductClientHub.API.Infrastructure;

namespace SLAProjectHub.API.UseCases.Projeto.Register
{
    public class ProjetoCodigoService
    {
        private readonly ProductClientHubDbContext _context;

        public ProjetoCodigoService(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public string GerarCodigo()
        {
            var ultimoProjeto = _context.Projeto
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            int novoNumero = 1;
            if (ultimoProjeto != null && !string.IsNullOrEmpty(ultimoProjeto.Codigo))
            {
                var codigoExistente = ultimoProjeto.Codigo.Replace("PROJ", "");
                if (int.TryParse(codigoExistente, out int numero))
                {
                    novoNumero = numero + 1;
                }
            }

            return $"PROJ{novoNumero:D3}";
        }
    }

}
