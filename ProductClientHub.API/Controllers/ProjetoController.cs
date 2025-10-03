using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using System;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        [HttpPost("{usuarioId}")]
        [ProducesResponseType(typeof(ResponseShortProjetoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Register(
            [FromRoute] Guid usuarioId,
            [FromBody] RequestProjetoJson request,
            [FromServices] RegisterProjetoUseCase useCase) // ✅ UseCase injetado
        {
            var response = useCase.Execute(usuarioId, request);
            return Created(string.Empty, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteProjetoUseCase useCase) // ✅ UseCase injetado
        {
            useCase.Execute(id);
            return NoContent();
        }
    }
}
