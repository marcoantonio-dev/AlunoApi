using AlunoApi.Model;
using AlunoApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace AlunoApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);

            }
            catch
            {

                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter alunos");

            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName(string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByName(nome);
                if (alunos == null)
                    return NotFound($"Não existem alunos com o critério {nome}");
                ; return Ok(alunos);

            }
            catch
            {

                return BadRequest("Request inválido");

            }
        }
        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Não existe aluno com id = {id}");

                return Ok(aluno);
            }
            catch
            {

                return BadRequest("Request inválido");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch
            {

                return BadRequest("Request inválido");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAluno(int id, [FromBody] Aluno aluno)
        {
            try
            {

                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    //return NoContent();
                    return Ok($"Aluno com id = {id} foi atualizado");
                }
                else
                    return BadRequest("Dados incosistentes");
            }
            catch
            {

                return BadRequest("Request Inválido");

            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {

                var aluno = await _alunoService.GetAluno(id);
                if (aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"O aluno com id = {id} foi excluido com sucesso");
                }
                else
                {
                    return NotFound($"Aluno com id = {id} não encontrado");
                }
            }
            catch 
            {

                return BadRequest("Request inválido");
            }
        }
    }
}