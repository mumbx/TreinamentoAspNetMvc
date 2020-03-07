using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.ADONET.DAL
{
    public class ClienteDAL
    {
        Repository repoDB;

        public bool Add(string nome, string email, string observacao = "")
        {
            if (repoDB ==  null)
            {
                repoDB = new Repository();
            }

            repoDB.Conn.ConnectionString = "Data Source=3P47_06;Initial Catalog=Livraria;User ID=sa;Password=Imp@ct@";

            repoDB.Cmd.CommandText = "@insert into Cliente (nOME, email, observacao) values('@nome', '@email', '@observacao')";


            repoDB.Cmd.Parameters.AddWithValue("@nome", nome);
            repoDB.Cmd.Parameters.AddWithValue("@email", email);
            repoDB.Cmd.Parameters.AddWithValue("@obervacao", observacao);

            repoDB.Cmd.Connection = repoDB.Conn;

            repoDB.OpenConnection();

            var retorno = repoDB.Cmd.ExecuteNonQuery();

            return (retorno > 0 ? true : false);

        }
    }
}
