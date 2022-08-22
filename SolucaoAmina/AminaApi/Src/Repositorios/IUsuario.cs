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
        Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task NovaUsuarioAsync(Usuario usuario);
        #endregion
    }
}
