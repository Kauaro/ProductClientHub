using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;
using SLAProjectHub.API.UseCases.Avaliacao.GetByCodigo;
using SLAProjectHub.API.UseCases.Avaliacao.GetByMatricula;
using SLAProjectHub.API.UseCases.Avaliacao.Register;
using SLAProjectHub.Communication.Requests;
using SLAProjectHub.Communication.Responses.Avaliacao;
using SLAProjectHub.Communication.Responses.Projeto;
using SLAProjectHub.Communication.Responses.Usuario;

namespace SLAProjectHub.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class AvaliacaoController : ControllerBase
        {
            private readonly ProductClientHubDbContext _context;
            private readonly RegisterAvaliacaoUseCase _register;
            private readonly GetAvaliacaoByMatricula _getByMatricula;
            private readonly GetAvaliacaoByCodigo _getByCodigo;



            public AvaliacaoController(
                ProductClientHubDbContext context,
                RegisterAvaliacaoUseCase register,
                GetAvaliacaoByMatricula getByMatricula,
                GetAvaliacaoByCodigo getByCodigo)
            {
                _context = context;
                _register = register;
                _getByMatricula = getByMatricula;
                _getByCodigo = getByCodigo;

            }

            [HttpPost("{projetoCodigo}")]
            [ProducesResponseType(typeof(ResponseShortUsuarioJson), StatusCodes.Status201Created)]
            [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
            public IActionResult Register(
                [FromRoute] string projetoCodigo,
                [FromBody] RequestAvaliacaoJson request)
            {
                var response = _register.Execute(projetoCodigo, request);
                return Created(string.Empty, response);
            }

        [HttpGet("matricula/{alunoMatricula}")]
        [ProducesResponseType(typeof(IEnumerable<ResponseAvaliacaoJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByMatricula([FromRoute] string alunoMatricula)
        {
            var response = await _getByMatricula.Execute(alunoMatricula);

            if (response == null || !response.Any())
                return NoContent();

            return Ok(response);
        }

        [HttpGet("codigo/{codigoProjeto}")]
        [ProducesResponseType(typeof(IEnumerable<ResponseAvaliacaoJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByCodigo([FromRoute] string codigoProjeto)
        {
            var response = await _getByCodigo.Execute(codigoProjeto);

            if (response == null || !response.Any())
                return NoContent();

            return Ok(response);
        }


    }
}
