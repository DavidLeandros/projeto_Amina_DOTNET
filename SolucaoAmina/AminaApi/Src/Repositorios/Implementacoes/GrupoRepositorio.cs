using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;

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

        public async Task NovoGrupoAsync(Grupo grupo)
        {
            if (!ExisteIdUsuario(grupo.Usuario.Id)) throw new Exception("Id Usuário não existe!");

            await _contexto.Grupos.AddAsync(new Grupo
            {
                Titulo = grupo.Titulo,
                Descricao = grupo.Descricao,
                Topico = grupo.Topico,
                Midia = grupo.Midia,
                Usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == grupo.Usuario.Id)

            });
            await _contexto.SaveChangesAsync();

            //Auxiliar
            bool ExisteIdUsuario(int Id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == Id);
                return auxiliar != null;
            }
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
