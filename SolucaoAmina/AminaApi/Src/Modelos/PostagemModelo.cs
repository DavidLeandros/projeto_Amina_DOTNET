using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AminaApi.Src.Modelos
{
    /// <summary>
    /// <para> Classe responsável por representar tb_postagens no banco.</para>
    /// </summary>
    [Table("tb_postagens")]
    public class Postagem
    {
        #region Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }

        [ForeignKey("fk_usuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("fk_grupo")]
        public Grupo Grupo { get; set; }
        #endregion
    }
}
