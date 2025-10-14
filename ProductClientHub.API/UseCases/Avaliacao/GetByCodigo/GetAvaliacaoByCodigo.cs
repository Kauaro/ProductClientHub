using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.Communication.Responses.Avaliacao;

namespace SLAProjectHub.API.UseCases.Avaliacao.GetByCodigo
{
    public class GetAvaliacaoByCodigo
    {
        private readonly ProductClientHubDbContext _context;

        public GetAvaliacaoByCodigo(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResponseAvaliacaoJson>> Execute(string codigoProjeto)
        {
            var avaliacoes = await _context
                .Avaliacao
                .AsNoTracking()
                .Include(a => a.Aluno)
                .Include(a => a.Projeto)
                .Where(a => a.Projeto.Codigo == codigoProjeto)
                .ToListAsync();

            if (avaliacoes == null || !avaliacoes.Any())
                throw new NotFoundException("Avaliações não encontradas.");

            return avaliacoes.Select(entity => new ResponseAvaliacaoJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
                AlunoMatricula = entity.Aluno.Matricula,
                Descricao = entity.Descricao,
                Nota = entity.Nota,
                ProjetoNome = entity.Projeto.Nome
            });
        }
    }
}
