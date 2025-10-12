using Azure.Core;
using FluentValidation;
using SLAProjectHub.Communication.Requests;

namespace SLAProjectHub.API.UseCases.Avaliacao.Validator
{
    public class RequestAvaliacaoValidator : AbstractValidator<RequestAvaliacaoJson>
    {
        public RequestAvaliacaoValidator()
        {

            RuleFor(avaliacao => avaliacao.Nome).NotEmpty().WithMessage("O nome não pode ser vazio");
            RuleFor(avaliacao => avaliacao.Descricao).NotEmpty().WithMessage("Você precisa informar uma descrição da nota.");
            RuleFor(avaliacao => avaliacao.Nota).NotEmpty().WithMessage("Você precisa dar uma nota.");
            RuleFor(avaliacao => avaliacao.AlunoMatricula).NotEmpty().WithMessage("Você precisa informar sua matricula de um projeto.");

        }
    }
}
