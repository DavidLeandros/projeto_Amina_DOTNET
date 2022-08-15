using System.Collections.Generic;
using System.Threading.Tasks;
using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;

namespace AminaApi.Src.Repositorios.Implementacoes
{
    public class GrupoRepositorio : IGrupo
    {
        #region Atributos
        private readonly AminaContextos _contexto;
        #endregion

        #region Construtores
        public GrupoRepositorio(AminaContextos contextos) 
        { 
            _contexto = contextos;
        }

        #endregion

        #region Método

        public Task AtualizarGrupoAsync(Grupo grupo)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeletarGrupoAsync(int id)
        {
            _contexto.Grupos.Remove(await PegarGruposPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        public Task NovoGrupoAsync(Grupo grupo)
        {
            throw new System.NotImplementedException();
        }

        public Task<Grupo> PegarGruposPeloIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Grupo>> PegarTodosGruposAsync()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
