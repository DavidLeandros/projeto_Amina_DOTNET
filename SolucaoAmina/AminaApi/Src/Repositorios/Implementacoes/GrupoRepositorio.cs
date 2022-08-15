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

        public async Task AtualizarGrupoAsync(Grupo grupo)
        {
            if (!ExisteIdUsuario(grupo.Usuario.Id)) throw new Exception("Id do grupo não encontrado");

            var grupoExiste = await PegarGruposPeloIdAsync(grupo.Id);
            grupoExiste.Titulo = grupo.Titulo;
            grupoExiste.Descricao = grupo.Descricao;
            grupoExiste.Topico = grupo.Topico;
            grupoExiste.Midia = grupo.Midia;
            
            _contexto.Grupos.Update(grupoExiste);
            await _contexto.SaveChangesAsync();

            //Auxiliar
            bool ExisteIdUsuario(int id)
            {
                var auxiliar  = _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
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
            
        }

        public async Task<List<Grupo>> PegarTodosGruposAsync()
        {
            return await _contexto.Grupos
                 .Include(g => g.Usuario)
                 .ToListAsync();
        }

        #endregion

    }
}
