using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Products.Delete
{
    public class DeleteProjetoUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public DeleteProjetoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid id)
        {
            var entity = _context.Projeto.FirstOrDefault(projeto => projeto.Id == id);

            if (entity is null)
                throw new NotFoundException("Projeto não encontrado");

            _context.Projeto.Remove(entity);
            _context.SaveChanges();
        }
    }
}
