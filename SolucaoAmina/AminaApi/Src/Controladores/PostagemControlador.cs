using AminaApi.Src.Modelos;
using AminaApi.Src.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AminaApi.Src.Controladores
{
    [ApiController]
    [Route("api/Postagens")]
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
        /// <summary>
        /// Pegar todas as postagens
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet("todasPostagens")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodasPostagemAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar postagem pelo Id
        /// </summary>
        /// <param name="idPostagem">Id da postagem</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o postagem</response> 
        /// <response code="404">Id não existente</response>
        [HttpGet("idPostagem/{idPostagem}")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
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

        /// <summary>
        /// Criar nova postagem
        /// </summary>
        /// <param name="postagem">Construtor para criar postagens</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição: 
        ///     
        ///     Post /api/Postagens/cadastrar
        ///     {
        ///         "titulo": "",
        ///         "descricao": "",
        ///         "topico": "",
        ///         "foto": "",
        ///         "Usuario": {
        ///             "id": 
        ///         },
        ///         "Grupo": {
        ///             "id":
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Retorna grupo criado</response> 
        /// <response code="401">Postagem já cadastrado</response>
        [HttpPost("cadastrarPostagem")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
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

        /// <summary>
        /// Atualizar postagem
        /// </summary>
        /// <param name="postagem">Construtor para atualizar postagem</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição: 
        ///     
        ///     Put /api/Postagens/cadastrarPostagem
        ///     {
        ///         "id": 0,
        ///         "titulo": "",
        ///         "descricao": "",
        ///         "topico": "",
        ///         "foto": ""
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Retorna postagem atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut("atualizarPostagem")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
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

        /// <summary>
        /// Deletar postagem
        /// </summary>
        /// <param name="idPostagem">Id da postagem</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Postagem deletada</response>
        /// <response code="404">Id da postagem não existe</response>
        [HttpDelete("idPostagem/{idPostagem}")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
        public async Task<ActionResult> DeletarPostagem([FromRoute] int idPostagem)
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
