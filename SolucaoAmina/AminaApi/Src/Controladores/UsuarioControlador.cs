using AminaApi.Src.Modelos;
using AminaApi.Src.Repositorios;
using AminaApi.Src.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AminaApi.Src.Controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos
        private readonly IUsuario _repositorio;
        private readonly IAutenticacao _servicos;
        #endregion

        #region Construtor
        public UsuarioControlador(IUsuario repositorio, IAutenticacao servicos)
        {
            _repositorio = repositorio;
            _servicos = servicos;
        }
        #endregion

        #region Métodos
        /// <summary> 
        /// Pegar usuario pelo CPF
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> PegarTodosUsuarioAsync()
        {
            var lista = await _repositorio.PegarTodosUsuarioAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary> 
        /// Pegar usuario pelo CPF
        /// </summary> 
        /// <param name="usuarioCpf">CPF do usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">Retorna o usuario</response> 
        /// <response code="404">CPF não existente</response>
        [HttpGet("cpf/{usuarioCpf}")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloCPFAsync([FromRoute] string usuarioCpf)
        {
            var usuario = await _repositorio.PegarUsuarioPeloCPFAsync(usuarioCpf);
            if (usuario == null) return NotFound(new { Mensagem = "Usuário não encontrado" });
            return Ok(usuario);
        }

        /// <summary>
        /// Criar novo Usuario 
        /// </summary> 
        /// <param name="usuario">Contrutor para criar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Usuarios/cadastrar 
        ///     { 
        ///         "cpf": "111.222.333-44",
        ///         "nome": "Nome do Usuario", 
        ///         "genero": "Feminino", 
        ///         "senha": "134652", 
        ///         "url_foto": "URLFOTO", 
        ///         "tipo": "NORMAL",
        ///         "data_nascimento": "2022-08-19T11:07:37.470Z"
        ///     } 
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response> 
        /// <response code="401">E-mail ja cadastrado</response>
        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario);
                return Created($"api/Usuarios/email/{usuario.CPF}", usuario);

            }catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary> 
        /// <param name="usuario">Contrutor para atualizar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Usuarios/cadastrar 
        ///     { 
        ///         "id": 0,
        ///         "cpf": "111.222.333-44",
        ///         "nome": "Nome do Usuario", 
        ///         "genero": "Feminino", 
        ///         "senha": "134652", 
        ///         "url_foto": "URLFOTO", 
        ///         "tipo": "NORMAL",
        ///         "data_nascimento": "2022-08-19T11:07:37.470Z"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Retorna usuario atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _repositorio.AtualizarUsuarioAsync(usuario);
                return Ok(usuario);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary> 
        /// Pegar Autorização 
        /// </summary> 
        /// <param name="usuario">Construtor para logar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        ///     POST /api/Usuarios/logar 
        ///     { 
        ///         "CPF": "111.222.333-44", 
        ///         "senha": "134652" 
        ///     } 
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response> 
        /// <response code="401">E-mail ou senha invalido</response>
        [HttpPost("logar")]
        [AllowAnonymous]
        public async Task<ActionResult> LogarAsync([FromBody] Usuario usuario)
        {
            var auxiliar = await _repositorio.PegarUsuarioPeloCPFAsync(usuario.CPF);
            if (auxiliar == null) return Unauthorized(new
            {
                Mensagem = "E-mail invalido"
            });
            if (auxiliar.Senha != _servicos.CodificarSenha(usuario.Senha))
                return Unauthorized(new { Mensagem = "Senha invalida" });

            var token = "Bearer " + _servicos.GerarToken(auxiliar);
            return Ok(new { Usuario = auxiliar, Token = token });
        }
        #endregion
    }
}
