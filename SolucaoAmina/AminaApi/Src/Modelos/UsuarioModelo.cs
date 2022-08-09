using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AminaApi.Src.Modelos
{
    [Table("tb_usuarios")]
    public class Usuario
    {
        #region Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Senha { get; set; }
        public string URL_Foto { get; set; }
        public DateTime Data_Nascimento { get; set; }
        #endregion
    }
}
