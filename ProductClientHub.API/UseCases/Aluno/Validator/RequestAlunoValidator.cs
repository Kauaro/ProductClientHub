using FluentValidation;
using SLAProjectHub.API.Entities;
using SLAProjectHub.Communication.Requests;

namespace SLAProjectHub.API.UseCases.Aluno.Validator
{
    public class RequestAlunoValidator : AbstractValidator<RequestAlunoJson>
    {
        public RequestAlunoValidator()
        {

            RuleFor(aluno => aluno.Nome).NotEmpty().WithMessage("O nome não pode ser vazio");
            RuleFor(aluno => aluno.Email).EmailAddress().WithMessage("O e-mail não é válido.");
            RuleFor(aluno => aluno.Senha).NotEmpty().WithMessage("Você precisa de uma senha.");
            RuleFor(aluno => aluno.Matricula).NotEmpty().WithMessage("Você precisa informar a matricula.");


        }
    }
}
