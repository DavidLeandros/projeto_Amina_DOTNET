using AminaApi.Src.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AminaApi.Src.Modelos
{
    /// <summary>
    /// <para> Classe responsável por representar tb_usuarios no banco.</para>
    /// </summary>
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

        public string UrlFoto { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }

        public DateTime DataNascimento { get; set; }

        [JsonIgnore, InverseProperty("usuario")]
        public List<Postagem> MinhasPostagens { get; set; }
        #endregion
    }

    public class UserLogin
    {
        public string CPF { get; set; }
        public string Senha { get; set; }
    }
}
