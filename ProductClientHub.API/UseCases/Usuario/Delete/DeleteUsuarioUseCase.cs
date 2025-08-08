using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Clients.Delete
{
    public class DeleteUsuarioUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();

            var entity = dbContext.Usuario.FirstOrDefault(usuario => usuario.Id == id);
            if (entity is null)
                throw new NotFoundException("Usuario não encontrado");

            dbContext.Usuario.Remove(entity);

            dbContext.SaveChanges();
        }
    }
}
