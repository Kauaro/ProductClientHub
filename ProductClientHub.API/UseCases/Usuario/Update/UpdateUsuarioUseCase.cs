using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Clients.Update
{
    public class UpdateUsuarioUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public UpdateUsuarioUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid usuarioId, RequestUsuarioJson request)
        {
            Validate(request);

            var usuario = _context.Usuario.ToList();

            var entity =_context.Usuario.FirstOrDefault(usuario => usuario.Id == usuarioId);
            if (entity is null)
                throw new NotFoundException("Usuario não encontrado.");

            entity.Nome = request.Nome;
            entity.Email = request.Email;
            entity.Senha = request.Senha;
            entity.Matricula = request.Matricula;
            entity.NivelAcesso = request.NivelAcesso;

            _context.Usuario.Update(entity);
            _context.SaveChanges();
        }

        private void Validate(RequestUsuarioJson request)
        {
            var validator = new RequestUsuarioValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
