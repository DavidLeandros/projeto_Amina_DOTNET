using System.Collections.Generic;
using System.Threading.Tasks;
using AminaApi.Src.Modelos;

namespace AminaApi.Src.Repositorios
{
    /// <summary>
    /// <para> Responsavel por representar ações de CRUD de grupo</para>
    /// </summary>
    public interface IGrupo
    {
        #region Métodos
        Task<List<Grupo>> PegarTodosGruposAsync();
        Task<Grupo> PegarGruposPeloIdAsync(int id);
        Task NovoGrupoAsync(Grupo grupo);
        Task AtualizarGrupoAsync(Grupo grupo);
        Task DeletarGrupoAsync(int id);
        #endregion
    }
}
