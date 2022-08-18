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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos
        private readonly IUsuario _repositorio;
        private readonly IAutenticacao _servicos;
        #endregion
        #region Construtores
        public UsuarioControlador(IUsuario repositorio, IAutenticacao servicos)
        {
            _repositorio = repositorio;
            _servicos = servicos;
        }
        #endregion

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
    }
}
