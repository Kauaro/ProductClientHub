using FluentValidation.Results;
using SLAProjectHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.Communication.Responses.Projeto;
using System.Linq;

namespace ProductClientHub.API.UseCases.Products.Register
{
    public class RegisterProjetoUseCase
    {
        private readonly ProductClientHubDbContext _context;

        private static readonly object _lock = new object();

        public RegisterProjetoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseShortProjetoJson Execute(Guid usuarioId, RequestProjetoJson request)
        {
            // ✅ Usa o DbContext injetado
            Validate(usuarioId, request);

            var entity = new Projeto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Tema = request.Tema,
                Aluno = request.Aluno,
                UsuarioId = usuarioId
            };

            lock (_lock)
            {
                _context.Projeto.Add(entity);
                _context.SaveChanges();
            }


            return new ResponseShortProjetoJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
            };
        }

        private void Validate(Guid usuarioId, RequestProjetoJson request)
        {
            // ✅ Usa o DbContext injetado
            var usuarioExist = _context.Usuario.Any(usuario => usuario.Id == usuarioId);
            if (!usuarioExist)
                throw new NotFoundException("Usuário não existe");

            var validator = new RequestProjetoValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
