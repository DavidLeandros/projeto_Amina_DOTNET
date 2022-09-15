using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <para> Resumo: Método assincrono para pegar vários usuários pelo Nome</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>ActionResult</returns>
        public async Task<List<Usuario>> PegarUsuariosPeloNomeAsync(string nome)
        {
            return await _contexto.Usuarios.Where(u => u.Nome.Contains(nome)).ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuário pelo Nome</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public async Task<Usuario> PegarUsuarioPeloNomeAsync(string nome)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Nome == nome);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuário pelo Email</para>
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Usuario> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(e => e.Email == email);
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para pegar um usuario pelo Id</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Usuario> PegarUsuarioPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id do usuário não foi encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            bool ExisteId(int id)
            {
                var aux = _contexto.Usuarios.FirstAsync(u => u.Id == id);
                return aux != null;
            }
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
                Email = usuario.Email,
                Nome = usuario.Nome,
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
            aux.Email = usuario.Email;
            aux.Senha = usuario.Senha;
            aux.UrlFoto = usuario.UrlFoto;
            aux.DataNascimento = usuario.DataNascimento;

            _contexto.Usuarios.Update(aux);
            await _contexto.SaveChangesAsync();
        }
        #endregion
    }
}
