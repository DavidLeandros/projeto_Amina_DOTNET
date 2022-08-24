using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios.Implementacoes
{
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos
        private readonly AminaContextos _contexto;
        #endregion

        #region Construtor
        public UsuarioRepositorio(AminaContextos contexto)
        {
            _contexto = contexto;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// <para> Resumo: Método assincrono para pegar todos os usuarios</para>
        /// </summary>
        /// <returns>ActionResult</returns>
        public async Task<List<Usuario>> PegarTodosUsuarioAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para pegar um usuario pelo CPF</para>
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>ActionResult</returns>
        public async Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.CPF == cpf);
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para criar usuario</para>
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>ActionResult</returns>
        public async Task NovoUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(new Usuario
            {
                CPF = usuario.CPF,
                Nome = usuario.Nome,
                Genero = usuario.Genero,
                Senha = usuario.Senha,
                UrlFoto = usuario.UrlFoto,
                Tipo = usuario.Tipo,
                DataNascimento = usuario.DataNascimento
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para atualizar usuario</para>
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>ActionResult</returns>
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            var aux = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            aux.Nome = usuario.Nome;
            aux.Genero = usuario.Genero;
            aux.Senha = usuario.Senha;
            aux.UrlFoto = usuario.UrlFoto;
            aux.Tipo = usuario.Tipo;
            aux.DataNascimento = usuario.DataNascimento;

            _contexto.Usuarios.Update(aux);
            await _contexto.SaveChangesAsync();
        }
        #endregion
    }
}
