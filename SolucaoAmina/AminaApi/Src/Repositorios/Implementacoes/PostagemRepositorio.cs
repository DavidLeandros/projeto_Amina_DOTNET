using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios.Implementacoes
{
    public class PostagemRepositorio : IPostagem
    {
        #region Atributos
        private readonly AminaContextos _contexto;
        #endregion

        #region Construtores
        public PostagemRepositorio(AminaContextos contextos) 
        { 
            _contexto = contextos;
        }
        #endregion

        #region Método
        public Task AtualizarPostagemAsync(Postagem postagem)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeletarPostagemAsync(int id)
        {
            _contexto.Postagens.Remove(await PegarPostagensPeloIdAsync(id));
            await _contexto.SaveChangesAsync();

        }

        public async Task NovaPostegemAsync(Postagem postagem)
        {

            if (!ExisteIdUsuario(postagem.Usuario.Id)) throw new Exception("id do Usuario não encontrado");

            if (!ExisteIdGrupo(postagem.Grupo.Id)) throw new Exception("Grupo não encontrado");

            await _contexto.Postagens.AddAsync(new Postagem
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Usuario = _contexto.Usuarios.FirstOrDefault(p => p.Id == postagem.Usuario.Id),
                Grupo = _contexto.Grupos.FirstOrDefault(g => g.Id == postagem.Grupo.Id)
            });
            await _contexto.SaveChangesAsync();
           
            //função auxiliar
            bool ExisteIdUsuario(int id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(p => p.Id == id);

                return auxiliar != null;
            }

            bool ExisteIdGrupo(int id)
            {
                var auxiliar = _contexto.Grupos.FirstOrDefault(g => g.Id == id);

                return auxiliar != null;
            }
        }
        
        public Task<Postagem> PegarPostagensPeloIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Postagem>> PegarTodasPostagemAsync()
        {
            return await _contexto.Postagens
                .Include(u => u.Usuario)
                .Include(g => g.Grupo)
                .ToListAsync();
        }
                
        #endregion
    }
}
