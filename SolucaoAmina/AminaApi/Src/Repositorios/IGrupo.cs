using System.Collections.Generic;
using System.Threading.Tasks;
using AminaApi.Src.Modelos;

namespace AminaApi.Src.Repositorios
{
    public interface IGrupo
    {
        Task<List<Grupo>> PegarTodosGruposAsync();
        Task<Grupo> PegarGruposPeloIdAsync(int id);
        Task NovoGrupoAsync(Grupo grupo);
        Task AtualizarGrupoAsync(Grupo grupo);
        Task DeletarGrupoAsync(int id);
    }
}
