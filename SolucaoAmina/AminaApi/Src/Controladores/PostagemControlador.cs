using AminaApi.Src.Modelos;
using AminaApi.Src.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AminaApi.Src.Controladores
{
    [Route("api/Postagens")]
    [ApiController]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos
        private readonly IPostagem _repositorio;
        #endregion

        #region Construtor
        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodasPostagemAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }
         
        [HttpGet("Id/{idPostagem}")]
        public async Task<ActionResult> PegarPostagemPeloId([FromRoute] int idPostagem)
        {
            try
            {
                return Ok(await _repositorio.PegarPostagensPeloIdAsync(idPostagem));
            } 
             catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> NovaPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.NovaPostagemAsync(postagem);
                return Created($"api/Postagens", postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.AtualizarPostagemAsync(postagem);
                return Ok(postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpDelete("id/{idPostagem}")]
        public async Task<ActionResult> DeletarPostagem([FromBody] int idPostagem)
        {
            try
            {
                await _repositorio.DeletarPostagemAsync(idPostagem);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion
    }
}
