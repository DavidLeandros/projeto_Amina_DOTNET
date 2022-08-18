using AminaApi.Src.Modelos;
using System.Threading.Tasks;

namespace AminaApi.Src.Servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
    }
}
