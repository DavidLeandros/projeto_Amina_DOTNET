using AminaApi.Src.Contexto;
using AminaApi.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminaTest.Contextos
{
    [TestClass]
    public class UusarioContextoTeste
    {
        #region Atributos

        private AminaContextos _contextos;

        #endregion

        #region Metodos

        [TestMethod]
        public void InserirNovoUsuario()
        {
            var opt = new DbContextOptionsBuilder<AminaContextos>()
                .UseInMemoryDatabase(databaseName: "IMD_Db_Amina_UCT1")
                .Options;

            _contextos = new AminaContextos(opt);

            DateTime data = new DateTime();

            _contextos.Usuarios.Add(new Usuario{
                CPF = "987.654.321-11",
                Nome = "Manoel",
                Genero = "Masculino",
                Senha = "asd",
                URL_Foto = "Url_Manoel"                

            }) ;
            _contextos.SaveChanges();

            var resultado = _contextos.Usuarios.FirstOrDefault(u => u.Nome == "Manoel");

            Assert.IsNotNull(resultado);
        }

        #endregion
    }
}
