using AminaApi.Src.Modelos;
using AminaApi.Src.Repositorios;
using AminaApi.Src.Servicos;
using Microsoft.AspNetCore.Authorization;
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
        /// Pegar todos os usuários
        /// </summary>
        /// <para> Resumo: Método assincrono para pegar todos os usuarios</para>
        /// <returns>ActionResult</returns> 
        /// <response code="200">Retorna todos os usuarios</response> 
        /// <response code="403">Usuario não autorizado</response>
        [HttpGet("todosUsuarios")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodosUsuarioAsync()
        {
            var lista = await _repositorio.PegarTodosUsuarioAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary> 
        /// Pegar usuários pelo Nome
        /// </summary> 
        /// <param name="usuariosNomes">Nome do usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">Usuario encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("nomes/{usuariosNomes}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuariosPeloNomeAsync([FromRoute] string usuariosNomes)
        {
            var usuario = await _repositorio.PegarUsuariosPeloNomeAsync(usuariosNomes);
            if (usuario == null) return NotFound(new { Mensagem = "Usuário não encontrado" });
            return Ok(usuario);
        }

        /// <summary> 
        /// Pegar usuário pelo Nome
        /// </summary> 
        /// <param name="usuarioNome">Nome do usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">Usuario encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("nome/{usuarioNome}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloNomeAsync([FromRoute] string usuarioNome)
        {
            var usuario = await _repositorio.PegarUsuarioPeloNomeAsync(usuarioNome);
            if (usuario == null) return NotFound(new { Mensagem = "Usuário não encontrado" });
            return Ok(usuario);
        }

        /// <summary>
        /// Pegar usuário pelo Id
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuario encontrado</response> 
        /// <response code="404">Id não existente</response>
        [HttpGet("idUsuario/{usuarioId}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int usuarioId)
        {
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(usuarioId);
            if(usuario == null) return NotFound(new { Mensagem = "Usuário não encontrado" });
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
        ///     POST /api/Usuarios/cadastrarUsuario 
        ///     { 
        ///         "email": "usuario@email.com",
        ///         "nome": "Nome do Usuario", 
        ///         "senha": "134652", 
        ///         "urlfoto": "URLFOTO", 
        ///         "tipo": "NORMAL",
        ///         "datanascimento": "2022-08-19"
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response> 
        /// <response code="422">Email ja cadastrado</response>
        [HttpPost("cadastrarUsuario")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario);
                return Created($"api/Usuarios/cpf/{usuario.Email}", usuario);

            }catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary> 
        /// <param name="usuario">Construtor para atualizar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     PUT /api/Usuarios/atualizarUsuario 
        ///     { 
        ///         "id": 0,
        ///         "email": "usuario@email.com",
        ///         "nome": "Nome do Usuario", 
        ///         "senha": "134652", 
        ///         "urlfoto": "URLFOTO",
        ///         "datanascimento": "2022-08-19"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Usuario atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut("atualizarUsuario")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
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
        /// 
        ///     POST /api/Usuarios/logar 
        ///     { 
        ///         "email": "usuario@email.com",
        ///         "senha": "134652" 
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="200">Usuario logado</response> 
        /// <response code="401">E-mail ou senha invalido</response>
        /// 
        [HttpPost("logar")]
        [AllowAnonymous]
        public async Task<ActionResult> LogarAsync([FromBody] UserLogin usuario)
        {
            var auxiliar = await _repositorio.PegarUsuarioPeloEmailAsync(usuario.Email);

            if (auxiliar == null) return Unauthorized(new
            {
                Mensagem = "Email inválido"
            });

            if (auxiliar.Senha != _servicos.CodificarSenha(usuario.Senha))
                return Unauthorized(new { Mensagem = "Senha inválida" });

            var token = "Bearer " + _servicos.GerarToken(auxiliar);
            return Ok(new { Usuario = auxiliar, Token = token });
        }
        #endregion
    }
}
