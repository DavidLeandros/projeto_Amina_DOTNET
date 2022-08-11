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
        #endregion
    }
}
