using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
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

        /// <summary>
        /// <para> Resumo: Método assincrono para pegar todos os usuarios</para>
        /// </summary>
        public async Task<List<Usuario>> PegarTodosUsuarioAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para pegar um usuario pelo CPF</para>
        /// </summary>
        public async Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf)
        {
            if (!ExisteCPF(cpf)) throw new Exception("CPF do usuário não foi encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.CPF == cpf);

            bool ExisteCPF(string cpf)
            {
                var aux = _contexto.Usuarios.FirstAsync(u => u.CPF == cpf);
                return aux != null;
            }
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para criar usuario</para>
        /// </summary>
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

        /// <summary>
        /// <para> Resumo: Método assincrono para atualizar usuario</para>
        /// </summary>
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            var aux = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            aux.Nome = usuario.Nome;
            aux.Genero = usuario.Genero;
            aux.Senha = usuario.Senha;
            aux.URL_Foto = usuario.URL_Foto;
            aux.Tipo = usuario.Tipo;
            aux.Data_Nascimento = usuario.Data_Nascimento;

            _contexto.Usuarios.Update(aux);
            await _contexto.SaveChangesAsync();
        }
    }
}
