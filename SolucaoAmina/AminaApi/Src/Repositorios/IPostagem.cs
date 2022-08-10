using AminaApi.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios
{
    public interface IPostagem
    {
        Task<List<Postagem>> PegarTodasPostagemAsync();
        Task<Postagem> PegarPostagensPeloIdAsync(int id);
        Task NovaPostegemAsync(Postagem postagem);
        Task AtualizarPostagemAsync(Postagem postagem);
        Task DeletarPostagemAsync(int id);

    }
}
