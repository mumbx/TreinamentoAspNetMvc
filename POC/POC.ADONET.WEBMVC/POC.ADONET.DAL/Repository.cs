using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// adiocoina os namespace do banco de dados
using System.Data.SqlClient;
using System.Data;

namespace POC.ADONET.DAL
{
    public class Repository
    {
        SqlConnection conn;
        SqlCommand cmd;

        public SqlCommand Cmd { get; set; }
        public SqlConnection Conn { get; set; }

        public Repository()
        {
            Conn = new SqlConnection();
            Cmd = new SqlCommand();
        }

        public bool OpenConnection()
        {
            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
