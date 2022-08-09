using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AminaApi.Src.Modelos
{
    public class GrupoModelo
    {
        [Table("tb_grupo")]
        public class Tema
        {
            #region Atributos

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            public string Titulo { get; set; }

            public string Descricao { get; set; }

            public string Topico { get; set; }

            public string Midia { get; set; }


            #endregion
        }
    }
}