using ViagensOnlineMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ViagensOnlineMvc.Db
{
    public class ViagensOnLineDb : DbContext
    {
        private const string conexao =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                C:\Antonio\github_repositorio\TreinamentoAspNetMvc\ViagensOnlineMvc\App_Data\ViagensOnLineDb.mdf;
            Integrated Security=True";
        public ViagensOnLineDb()
        : base(conexao)
        { }
        public DbSet<Destino> Destinos { get; set; }
    }
}
    
