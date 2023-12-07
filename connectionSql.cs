using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm
{
    internal class connectionSql
    {
        public static SqlConnection connect()
        {
            SqlConnection conn = new SqlConnection("Data Source=SAHAR\\SQLEXPRESS; Initial Catalog=RegisterStudent; Integrated Security=true;");
            conn.Open();
            return conn;
        }
    }
}
