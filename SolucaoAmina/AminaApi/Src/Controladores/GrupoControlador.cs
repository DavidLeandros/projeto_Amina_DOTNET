using AminaApi.Src.Modelos;
using AminaApi.Src.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AminaApi.Src.Controladores
{
    [ApiController]
    [Route("api/Grupos")]
    [Produces("application/json")]
    public class GrupoControlador : ControllerBase
    {

        #region Atributos

        private readonly IGrupo _repositorio;

        #endregion

        #region Construtores
        public GrupoControlador(IGrupo repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos
        /// <summary> 
        /// Pegar todos os grupos
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet("todosGrupos")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodosGruposAsync()
        {
            var lista = await _repositorio.PegarTodosGruposAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar grupo pelo Id
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o grupo</response> 
        /// <response code="404">Id não existente</response>
        [HttpGet("idGrupo/{id}")]
        [Authorize(Roles = "NORMAL, ADMINISTRADOR")]
        public async Task<ActionResult> PegarGruposPeloIdAsync([FromRoute] int id)
        {
            try
            {
                return Ok(await _repositorio.PegarGruposPeloIdAsync(id));
            }
            catch(Exception ex)
            {
                return NotFound(new {Mensagem = ex.Message});
            }
        }

        /// <summary>
        /// Criar novo Grupo
        /// </summary>
        /// <param name="grupo">Construtor para criar grupo</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição: 
        ///     
        ///     Post /api/Grupos/cadastrar
        ///     {
        ///         "titulo": "",
        ///         "descricao": "",
        ///         "topico": "",
        ///         "midia": "",
        ///         "Usuario":{
        ///             "id": 
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Retorna grupo criado</response> 
        /// <response code="401">grupo ja cadastrado</response>
        [HttpPost("cadastrarGrupo")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> NovoGrupoAsync([FromBody] Grupo grupo)
        {
            try
            {
                await _repositorio.NovoGrupoAsync(grupo);
                return Created($"api/Grupos/{grupo.Titulo}", grupo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar Grupo
        /// </summary>
        /// <param name="grupo">Construtor para atualizar grupo</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição: 
        ///     
        ///     Put /api/Grupos
        ///     {
        ///         "id": 0,
        ///         "titulo": "",
        ///         "descricao": "",
        ///         "topico": "",
        ///         "midia": "",
        ///         "usuario": ""
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Retorna grupo atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut("atualizarGrupo")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarGrupo([FromBody] Grupo grupo)
        {
            try
            {
                await _repositorio.AtualizarGrupoAsync(grupo);
                return Ok(grupo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar grupo pelo Id
        /// </summary>
        /// <param name="idGrupo">Id do grupo</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Grupo deletado</response>
        /// <response code="404">Id do grupo não existe</response>
        [HttpDelete("deletarGrupo/{idGrupo}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarGrupo([FromRoute] int idGrupo)
        {
            try
            {
                await _repositorio.DeletarGrupoAsync(idGrupo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        #endregion

    }
}
