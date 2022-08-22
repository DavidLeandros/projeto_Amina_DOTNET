using AminaApi.Src.Modelos;
using System.Threading.Tasks;

namespace AminaApi.Src.Servicos
{
    /// <summary>
    /// <para> Responsavel por representar ações para a autenticação</para>
    /// </summary>
    public interface IAutenticacao
    {
        #region Métodos
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
        #endregion
    }
}
