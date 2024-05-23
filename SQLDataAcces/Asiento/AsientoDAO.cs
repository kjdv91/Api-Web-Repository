using log4net;
using Microsoft.Data.SqlClient;
using Prueba.Data;
using Prueba.DataAccess;
using Prueba.Models;
using System.Data;
using System.Reflection;

namespace Prueba.SQLDataAcces.Asiento
{
    public class AsientoDAO: IAsientoDAO
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly ILoggerManager Log = new LoggerManager(Logger);
        ConsultasSQL sql = new ConsultasSQL();
        public async Task<List<AsientoModel>> GetAllAsientos()
        {
            List<AsientoModel> listaAsiento = new List<AsientoModel>();

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "contabilidad.obtenerAsientosAXIS";

            try
            {
                using (IDataReader reader = sql.ejecutaReader())
                {
                    while (reader.Read())
                    {
                        listaAsiento.Add(AsientoModel.AsientoDataReader(reader));
                    }
                }


            }
            catch (Exception exception)
            {
                Log.Error(exception.Message, exception);

                sql.cerrarConexion();
                throw;
            }
            finally { sql.cerrarConexion(); }

            return listaAsiento;
        }
        public async  Task<int> insertAsiento(AsientoModel asiento)
        {

            int idSolicitud = 0;

            using (ConsultasSQL sql = new ConsultasSQL())
            {

                try
                {
                    sql.Comando.CommandType = CommandType.StoredProcedure;
                    sql.Comando.CommandText = "contabilidad.insertarAsiento";
                    sql.Comando.Parameters.AddWithValue("@codigoCabecera", asiento.CodigoCabecera);
                    sql.Comando.Parameters.AddWithValue("@unidadAdministrativa", asiento.CodigoUnidadAdministreativa);
                    sql.Comando.Parameters.AddWithValue("@estado", asiento.Estado);
                    sql.Comando.Parameters.AddWithValue("@legado", asiento.Legado);
                    sql.Comando.Parameters.AddWithValue("@fechaContable", asiento.FechaContable);
                   
                    //sql.Comando.Parameters.AddWithValue("p_rec_fechaSolucionReclamo", DBNull.Value);
                   
                 


                    var idSolicitudRetorno = new SqlParameter("@IdSalida", SqlDbType.Int);
                    idSolicitudRetorno.Direction = ParameterDirection.Output;
                    sql.Comando.Parameters.Add(idSolicitudRetorno);
                    //idSolicitud = sql.ejecutaQuery();
                    idSolicitud = 1;

                }
                catch (Exception exception)
                {
                    Log.Error(exception.Message, exception);

                    sql.cerrarConexion();
                    throw;
                }
                finally
                {
                    sql.cerrarConexion();
                }
            }
            return idSolicitud;

        }



        public void UpdateAsiento(AsientoModel asiento)
        {
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_rec_aproducto_cliente";

            sql.Comando.Parameters.Add(new SqlParameter("e_id_product_client", asiento.CodigoCabecera));
            sql.Comando.Parameters.Add(new SqlParameter("e_description", asiento.CodigoUnidadAdministreativa));
            sql.Comando.Parameters.Add(new SqlParameter("e_status", asiento.Legado));
            sql.Comando.Parameters.Add(new SqlParameter("e_id_productq01", asiento.Estado));
            sql.Comando.Parameters.Add(new SqlParameter("e_user_modify", asiento.FechaContable));
           
            try
            {
                sql.ejecutaQuery();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                sql.cerrarConexion();
            }
        }

        public List<AsientoModel> ObtenerAsientoId(int id)
        {
            List <AsientoModel> asientoId = new List<AsientoModel>();
            sql = new ConsultasSQL();

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_rec_cproducto_cliente_id";
            sql.Comando.Parameters.Add(new SqlParameter("e_id_product_client", id));

            try
            {
                using (IDataReader data = sql.ejecutaReader())
                {
                    while (data.Read())
                    { 
                        asientoId.Add(AsientoModel.AsientoDataReader(data));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                sql.cerrarConexion();
            }

            return asientoId;
        }

    }
}
