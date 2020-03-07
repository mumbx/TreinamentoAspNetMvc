using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POC.ADONET.DAL;

namespace POC.TEST.UNITARIO
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestarInsertNaTabelaCliente()
        {
            ClienteDAL clienteDal = new ClienteDAL();


            clienteDal.Add("tonin", "tonindosbaloes@nenenene.com", "to atrazado");

         }
    }
}
