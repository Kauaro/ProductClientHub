using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using SLAProjectHub.Communication.Responses.Avaliacao;

namespace SLAProjectHub.API.UseCases.Avaliacao.GetAll
{
    public class GetAllAvaliacaoUseCase
    {
        private readonly ProductClientHubDbContext _context;

        public GetAllAvaliacaoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseAllAvaliacaoJson Execute()
        {
            var avaliacao = _context.Avaliacao
                .Include(p => p.Aluno)
                .Include(p => p.Projeto)
                .ToList();

            return new ResponseAllAvaliacaoJson
            {
                Avaliacao = avaliacao.Select(u => new ResponseShortAvaliacaoJson
                {
                    Nome = u.Nome,
                    AlunoMatricula = u.Aluno.Matricula,
                    Nota = u.Nota,
                    ProjetoNome = u.Projeto.Nome,
                }).ToList()
            };
        }
    }
}
