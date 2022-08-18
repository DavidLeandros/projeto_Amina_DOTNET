using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AminaApi.Src.Repositorios.Implementacoes
{
    public class UsuarioRepositorio : IUsuario
    {
        private readonly AminaContextos _contexto;
        public UsuarioRepositorio(AminaContextos contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Usuario>> PegarTodasUsuarioAsync()
        {

        }

        public async Task<Usuario> PegarUsuarioPeloIdAsync(int id)
        {

        }

        public async Task<Usuario>

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {

        }

        public async Task NovaUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(new Usuario
            {
                CPF = usuario.CPF,
                Nome = usuario.Nome,
                Genero = usuario.Genero,
                Senha = usuario.Senha,
                URL_Foto = usuario.URL_Foto,
                Tipo = usuario.Tipo,
                Data_Nascimento = usuario.Data_Nascimento
            });
            await _contexto.SaveChangesAsync();
        }

        
    }
}
