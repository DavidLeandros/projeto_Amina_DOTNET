using AminaApi.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios
{
    public interface IUsuario
    {
        Task<List<Usuario>> PegarTodosUsuarioAsync();
        Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task NovaUsuarioAsync(Usuario usuario);
    }
}
