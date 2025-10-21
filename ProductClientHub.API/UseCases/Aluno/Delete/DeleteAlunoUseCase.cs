using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;

namespace SLAProjectHub.API.UseCases.Aluno.Delete
{
    public class DeleteAlunoUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public DeleteAlunoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid id)
        {
            var entity = _context.Aluno.FirstOrDefault(aluno => aluno.Id == id);

            if (entity is null)
                throw new NotFoundException("Usuário não encontrado");

            _context.Aluno.Remove(entity);
            _context.SaveChanges();
        }
    }
}
