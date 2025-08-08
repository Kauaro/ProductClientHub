using FluentValidation;
using ProductClientHub.API.Entities;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Clients.SharedValidator
{
    public class RequestUsuarioValidator : AbstractValidator<RequestUsuarioJson>
    {

        public RequestUsuarioValidator()
        {

            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome não pode ser vazio");
            RuleFor(usuario => usuario.Email).EmailAddress().WithMessage("O e-mail não é válido.");
            RuleFor(usuario => usuario.Senha).NotEmpty().WithMessage("Você precisa de uma senha.");
            RuleFor(usuario => usuario.Matricula).NotEmpty().WithMessage("Você precisa informar a matricula.");


        }
    }
}