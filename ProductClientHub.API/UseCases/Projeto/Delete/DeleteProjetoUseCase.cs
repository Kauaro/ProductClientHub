using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Products.Delete
{
    public class DeleteProjetoUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();

            var entity = dbContext.Projeto.FirstOrDefault(product => product.Id == id);
            if (entity is null)
                throw new NotFoundException("Projeto não encontrado");

            dbContext.Projeto.Remove(entity);

            dbContext.SaveChanges();
        }
    }
}
