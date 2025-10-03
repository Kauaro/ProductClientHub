using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Clients.Delete
{
    public class DeleteUsuarioUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public DeleteUsuarioUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid id)
        {
            var entity = _context.Usuario.FirstOrDefault(usuario => usuario.Id == id);

            if (entity is null)
                throw new NotFoundException("Usuário não encontrado");

            _context.Usuario.Remove(entity);
            _context.SaveChanges();
        }
    }
}
