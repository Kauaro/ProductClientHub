using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Products.Register
{
    public class RegisterProjetoUseCase
    {
        public ResponseShortProjetoJson Execute(Guid usuarioId, RequestProjetoJson request)
        {
            var dbContext = new ProductClientHubDbContext();

            Validate(dbContext, usuarioId, request);

            var entity = new Projeto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Tema = request.Tema,
                Aluno = request.Aluno,
                UsuarioId = usuarioId
            };

            dbContext.Projeto.Add(entity);

            dbContext.SaveChanges();

            return new ResponseShortProjetoJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
            };
        }

        private void Validate(ProductClientHubDbContext dbContext, Guid usuarioId, RequestProjetoJson request)
        {
            var usuarioExist = dbContext.Usuario.Any(usuario => usuario.Id == usuarioId);
            if (usuarioExist == false)
                throw new NotFoundException("Usuario não existe");

            var validator = new RequestProjetoValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
