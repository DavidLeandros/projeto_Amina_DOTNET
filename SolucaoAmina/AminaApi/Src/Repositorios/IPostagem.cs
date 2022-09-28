using AminaApi.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios
{
    /// <summary>
    /// <para> Responsavel por representar ações de CRUD de postagem</para>
    /// </summary>
    public interface IPostagem
    {
        #region Métodos
        Task<List<Postagem>> PegarTodasPostagemAsync();
        Task<List<Postagem>> PegarTodasPostagemPorGrupoAsync(int idGrupo);
        Task<Postagem> PegarPostagensPeloIdAsync(int id);
        Task NovaPostagemAsync(Postagem postagem);
        Task AtualizarPostagemAsync(Postagem postagem);
        Task DeletarPostagemAsync(int id);
        #endregion
    }
}
