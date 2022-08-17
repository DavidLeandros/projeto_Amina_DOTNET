using AminaApi.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AminaApi.Src.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public async Task<ActionResult> PegarTodosGruposAsync()
        {
            var lista = await _repositorio.PegarTodosGruposAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        [HttpGet("id/{id}")]
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
        #endregion

    }
}
