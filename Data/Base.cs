using Microsoft.Data.SqlClient;

namespace Prueba.Data
{
    public class Base
    {

        public static SqlConnection ObtenerConexionSQL()
        {
            
            string conSql = "Server=jbg01ds26;Database=CTD;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True; Encrypt=False"; 

            //log de la conexion
            Console.WriteLine("conexionSQL: " + conSql);

            try
            {


                return new SqlConnection(conSql);
            }
            catch
            {
                throw;
            }
        }
    }

}
