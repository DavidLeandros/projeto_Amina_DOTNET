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

        public Task AtualizarUsuarioAsync(Usuario usuario)
        {
            throw new System.NotImplementedException();
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

        public Task<List<Usuario>> PegarTodasUsuarioAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }
}
