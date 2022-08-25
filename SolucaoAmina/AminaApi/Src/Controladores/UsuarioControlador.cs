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
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodosUsuarioAsync()
        {
            var lista = await _repositorio.PegarTodosUsuarioAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary> 
        /// Pegar usuário pelo CPF
        /// </summary> 
        /// <param name="usuarioCpf">CPF do usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">Usuario encontrado</response> 
        /// <response code="404">CPF não existente</response>
        [HttpGet("cpf/{usuarioCpf}")]
        [Authorize(Roles = "ADMINISTRADOR")]
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
        ///         "cpf": "11122233344",
        ///         "nome": "Nome do Usuario", 
        ///         "genero": "Feminino", 
        ///         "senha": "134652", 
        ///         "urlfoto": "URLFOTO", 
        ///         "tipo": "NORMAL",
        ///         "datanascimento": "2022-08-19T11:07:37.470Z"
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response> 
        /// <response code="422">CPF ja cadastrado</response>
        [HttpPost("cadastrar")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]

        public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario);
                return Created($"api/Usuarios/cpf/{usuario.CPF}", usuario);

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
        ///     PUT /api/Usuarios/cadastrar 
        ///     { 
        ///         "id": 0,
        ///         "cpf": "11122233344",
        ///         "nome": "Nome do Usuario", 
        ///         "genero": "Feminino", 
        ///         "senha": "134652", 
        ///         "urlfoto": "URLFOTO", 
        ///         "tipo": "NORMAL",
        ///         "datanascimento": "2022-08-19T11:07:37.470Z"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Usuario atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut]
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
        ///         "cpf": "11122233344", 
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
            var auxiliar = await _repositorio.PegarUsuarioPeloCPFAsync(usuario.CPF);

            if (auxiliar == null) return Unauthorized(new
            {
                Mensagem = "CPF inválido"
            });

            if (auxiliar.Senha != _servicos.CodificarSenha(usuario.Senha))
                return Unauthorized(new { Mensagem = "Senha inválida" });

            var token = "Bearer " + _servicos.GerarToken(auxiliar);
            return Ok(new { Usuario = auxiliar, Token = token });
        }
        #endregion
    }
}
