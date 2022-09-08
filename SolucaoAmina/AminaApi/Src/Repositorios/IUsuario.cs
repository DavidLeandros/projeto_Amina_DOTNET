using AminaApi.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios
{
    /// <summary>
    /// <para> Responsavel por representar ações de CRUD de usuario</para>
    /// </summary>
    public interface IUsuario
    {
        #region Métodos
        Task<List<Usuario>> PegarTodosUsuarioAsync();
        Task<Usuario> PegarUsuarioPeloNomeAsync(string nome);
        Task<Usuario> PegarUsuarioPeloEmailAsync(string email);
        Task<Usuario> PegarUsuarioPeloIdAsync(int id);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task NovoUsuarioAsync(Usuario usuario);
        #endregion
    }
}
