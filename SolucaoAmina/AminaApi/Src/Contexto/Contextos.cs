using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AminaApi.Src.Contexto
{
    public class AminaContextos : DbContext
    {
        #region Atributos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        #endregion

        #region
        public AminaContextos(DbContextOptions<AminaContextos> opt) : base(opt)
        {
        }
        #endregion
    }
}
