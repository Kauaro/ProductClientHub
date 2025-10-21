using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using SLAProjectHub.API.Entities;
using SLAProjectHub.API.UseCases;
using SLAProjectHub.API.UseCases.Aluno.Delete;
using SLAProjectHub.API.UseCases.Aluno.GetAll;
using SLAProjectHub.API.UseCases.Aluno.Register;
using SLAProjectHub.API.UseCases.Aluno.Update;
using SLAProjectHub.Communication.Requests;
using SLAProjectHub.Communication.Responses.Aluno;

namespace SLAProjectHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly ProductClientHubDbContext _context;
        private readonly RegisterAluno _register;
        private readonly GetAllAluno _getAll;
        private readonly UpdateAlunoUseCase _update;
        private readonly DeleteAlunoUseCase _delete;
        public AlunoController(
            ProductClientHubDbContext context,
            RegisterAluno register,
            GetAllAluno getall,
            UpdateAlunoUseCase update, 
            DeleteAlunoUseCase delete
            )
        {
            _context = context;
            _register = register;
            _getAll = getall;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortAlunoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestAlunoJson request)
        {
            var response = _register.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RequestLoginJson request)
        {
            var aluno = _context.Aluno
                .FirstOrDefault(u => u.Matricula == request.Matricula);

            if (aluno == null)
            {
                return Unauthorized(new { message = "Matrícula ou senha inválidos." });
            }

            var passwordService = new PasswordService();

            var senhaCorreta = passwordService.VerifyPassword(request.Senha, aluno.Senha);

            if (!senhaCorreta)
            {
                return Unauthorized(new { message = "Matrícula ou senha inválidos." });
            }


            return Ok(new
            {
                message = "Login realizado com sucesso!",
                id = aluno.Id,
                nome = aluno.Nome,
                matricula = aluno.Matricula,
                email = aluno.Email,
                curso = aluno.Curso,
                periodo = aluno.Periodo,
                avaliacoes = new string[] { }
            });
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllAlunoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var response = _getAll.Execute();
            if (response.Aluno.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RequestAlunoJson request)
        {
            _update.Execute(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _delete.Execute(id);
            return NoContent();
        }
    }
}
